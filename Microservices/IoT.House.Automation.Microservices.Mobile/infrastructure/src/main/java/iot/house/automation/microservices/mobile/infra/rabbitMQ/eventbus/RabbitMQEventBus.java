package iot.house.automation.microservices.mobile.infra.rabbitMQ.eventbus;

import com.google.gson.Gson;
import com.rabbitmq.client.AMQP;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.DefaultConsumer;
import com.rabbitmq.client.Envelope;
import iot.house.automation.microservices.mobile.application.interfaces.EventBus;
import iot.house.automation.microservices.mobile.application.interfaces.EventHandler;
import iot.house.automation.microservices.mobile.application.messaging.events.BaseEvent;
import iot.house.automation.microservices.mobile.infra.rabbitMQ.connection.PersistedConnection;
import iot.house.automation.microservices.mobile.infra.rabbitMQ.subscription.SubscriptionManager;
import lombok.SneakyThrows;
import net.jodah.failsafe.Failsafe;
import net.jodah.failsafe.RetryPolicy;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.net.PortUnreachableException;
import java.net.SocketException;
import java.nio.charset.StandardCharsets;
import java.time.Duration;
import java.util.concurrent.TimeoutException;


@Component
public class RabbitMQEventBus implements EventBus {
    private final ApplicationContext context;
    private final PersistedConnection connection;
    private final SubscriptionManager subscriptionManager;
    private final Channel consumerChannel;
    private final Gson gson;

    @Autowired
    public RabbitMQEventBus(ApplicationContext context, PersistedConnection connection) {
        this.context = context;
        this.connection = connection;
        this.gson = new Gson();
        this.subscriptionManager = new SubscriptionManager();

        subscriptionManager.setOnEventAdded(s -> onSubscriptionManagerEventAdded(s));
        subscriptionManager.setOnEventRemoved(s -> onSubscriptionManagerEventRemoved(s));

        this.consumerChannel = createConsumerChannel();
    }

    @Override
    public void publish(BaseEvent event) {
        var policy = new RetryPolicy<>()
                .handle(Exception.class)
                .withMaxRetries(5)
                .withDelay(Duration.ofMinutes(1));

        try(var channel = connection.createChannel()) {

            var eventName = event.getType();
            var message = gson.toJson(event);
            var body = message.getBytes();

            Failsafe.with(policy).run(() -> {
                channel.basicPublish(connection.getConfig().getPublishExchange(), eventName, null, body);
            });


        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public <TEvent extends BaseEvent, TEventHandler extends EventHandler> void subscribe(Class<TEvent> eventType, Class<TEventHandler> handlerType) {
        subscriptionManager.addSubscription(eventType, handlerType);
    }

    @Override
    public <TEvent extends BaseEvent, TEventHandler extends EventHandler> void unsubscribe(Class<TEvent> eventType, Class<TEventHandler> handlerType) {
        subscriptionManager.removeSubscription(eventType, handlerType);
    }

    @SneakyThrows
    private Channel createConsumerChannel() {
        var channel = connection.createChannel();
        channel.exchangeDeclare(connection.getConfig().getPublishExchange(), connection.getConfig().getExchangeType(), true);
        return channel;
    }

    private void onSubscriptionManagerEventAdded(String eventName)
    {
        try(var channel = connection.createChannel()) {

            channel.queueDeclare(eventName, true, false, false, null);
            channel.queueBind(eventName, connection.getConfig().getListenerExchange(), eventName);
            createConsumer(consumerChannel, eventName);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void onSubscriptionManagerEventRemoved(String eventName)
    {
        try(var channel = connection.createChannel()) {

            channel.queueUnbind(eventName, connection.getConfig().getListenerExchange(), eventName);
            consumerChannel.basicCancel(eventName);

            if(!subscriptionManager.isEmpty()) return;

            consumerChannel.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @SneakyThrows
    private void createConsumer(Channel channel, String eventName) {
        boolean autoAck = false;
        channel.basicConsume(eventName, false, eventName,
                new DefaultConsumer(channel) {
                    @Override
                    public void handleDelivery(String consumerTag,
                                               Envelope envelope,
                                               AMQP.BasicProperties properties,
                                               byte[] body)
                            throws IOException
                    {
                        var routingKey = envelope.getRoutingKey();
                        var deliveryTag = envelope.getDeliveryTag();
                        var message = new String(body, StandardCharsets.UTF_8);

                        handleEvent(routingKey, message, deliveryTag, channel);
                        //channel.basicAck(deliveryTag, false);
                    }
                });
    }

    private void handleEvent(String eventName, String message, long deliveryTag, Channel channel){
        if(!subscriptionManager.hasSubscriptionForEvent(eventName)) {
            ackMessage(deliveryTag, channel);
        }

        var subs = subscriptionManager.getHandlersForEvent(eventName);

        subs.forEach(subscription -> {
            var result = subscription.handle(message, context);

            if(result.isCancelled()) {
                nackMessage(deliveryTag, channel);
            }else{
                ackMessage(deliveryTag, channel);
            }
        });
    }

    @SneakyThrows
    private void ackMessage(long deliveryTag, Channel channel) {
        channel.basicAck(deliveryTag, false);
    }

    @SneakyThrows
    private void nackMessage(long deliveryTag, Channel channel) {
        channel.basicNack(deliveryTag, false, true);
    }
}
