using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;
using IoT.House.Automation.Microservices.Arduino.Application.Util;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Connection;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Subscription;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.EventBus
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly IServiceProvider _provider;
        private readonly PersisterConnection _connection;
        private readonly SubscriptionManager _subscriptionManager;
        private readonly IModel _consumerChannel;

        public RabbitMQEventBus(IServiceProvider provider, PersisterConnection connection)
        {
            IfHasNullThrowException(provider, connection);

            _provider = provider;
            _connection = connection;

            _subscriptionManager = new SubscriptionManager();
            _subscriptionManager.OnEventRemoved += OnSubscriptionManagerEventRemoved;
            _subscriptionManager.OnEventAdded += OnSubscriptionManagerEventAdded;

            _consumerChannel = CreateConsumerChannel();

        }

        private void OnSubscriptionManagerEventAdded(object _, string eventName)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(eventName, true, false, false);

                channel.QueueBind(
                    queue: eventName,
                    exchange: _connection.Config.Exchange,
                    routingKey: eventName
                );

                CreateConsumer(_consumerChannel, eventName);
            }
        }

        private void OnSubscriptionManagerEventRemoved(object _, string eventName)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueUnbind(
                    queue: eventName,
                    exchange: _connection.Config.Exchange,
                    routingKey: eventName
                );

                _consumerChannel.BasicCancel(eventName);

                if (!_subscriptionManager.IsEmpty) return;
                _consumerChannel.Close();
            }
        }

        public void Publish(Event @event)
        {
            var policy = Policy.Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                });

            using (var channel = _connection.CreateModel())
            {
                var eventName = @event.GetType()
                    .Name;

                channel.ExchangeDeclare(exchange: _connection.Config.Exchange, type: _connection.Config.ExchangeType);

                var settings = NewtonsoftJsonUtil.UseCustomNewtonsoftSettings();

                var message = JsonConvert.SerializeObject(@event, settings);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: _connection.Config.Exchange,
                        routingKey: eventName,
                        basicProperties: null,
                        body: body);
                });
            }
        }

        public void Subscribe<TEvent, TEventHandler>() where TEvent : Event where TEventHandler : IEventHandler<TEvent>
        {
            _subscriptionManager.AddSubscription<TEvent, TEventHandler>();
        }

        public void Unsubscribe<TEvent, TEventHandler>() where TEvent : Event where TEventHandler : IEventHandler<TEvent>
        {
            _subscriptionManager.RemoveSubscription<TEvent, TEventHandler>();
        }

        private IModel CreateConsumerChannel()
        {

            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: _connection.Config.Exchange, type: _connection.Config.ExchangeType);
            return channel;
        }

        private void CreateConsumer(IModel channel, string queueName)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                await HandleEvent(eventName, message, ea.DeliveryTag, channel);
            };

            channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer,
                consumerTag: queueName);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
            };
        }

        private async Task HandleEvent(string eventName, string message, ulong deliveryTag, IModel channel)
        {
            if (!_subscriptionManager.HasSubscriptionsForEvent(eventName)) return;

            var subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);

            foreach (var subscription in subscriptions)
            {
                var result= subscription.Handle(message, _provider);

                if (result.IsFaulted || result.IsCanceled)
                {
                    channel.BasicNack(deliveryTag, false, true);
                }
                else
                {
                    channel.BasicAck(deliveryTag, false);
                }
            }

        }

        private void IfHasNullThrowException(params object[] args)
        {
            args.Any(o =>
            {
                if (o == default) throw new ArgumentNullException(nameof(o));
                return false;
            });
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }

    }
}
