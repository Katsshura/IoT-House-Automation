package iot.house.automation.microservices.mobile.infra.rabbitMQ.connection;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import iot.house.automation.microservices.mobile.infra.rabbitMQ.config.RabbitMQConfig;
import lombok.Getter;
import lombok.SneakyThrows;
import net.jodah.failsafe.Failsafe;
import net.jodah.failsafe.RetryPolicy;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.net.ConnectException;
import java.net.PortUnreachableException;
import java.net.SocketException;
import java.time.Duration;

@Component
public class PersistedConnection {
    private final ConnectionFactory connectionFactory;
    private final int retryCount;
    private Connection connection;
    private final Object syncRoot;

    @Getter
    private final RabbitMQConfig config;

    @Autowired
    public PersistedConnection(RabbitMQConfig config) {
        syncRoot = new Object();
        this.retryCount = config.getRetryCount();
        this.config = config;
        this.connectionFactory = this.configureConnectionFactory();
    }

    @SneakyThrows
    public Channel createChannel() {
        if (isConnected()) return connection.createChannel();

        if (!tryConnect()) throw new ConnectException("No RabbitMQ connections are available to perform this action");

        return connection.createChannel();
    }

    private boolean tryConnect() {
        synchronized (syncRoot) {
            var policy = new RetryPolicy<>()
                    .handle(SocketException.class)
                    .handle(PortUnreachableException.class)
                    .withMaxRetries(retryCount)
                    .withDelay(Duration.ofMinutes(1));
            Failsafe.with(policy).run(() -> {
                connection = connectionFactory.newConnection();
            });

            if (!isConnected()) return false;

            connection.addShutdownListener(cause -> tryConnect());

            return true;
        }
    }

    @SneakyThrows
    private ConnectionFactory configureConnectionFactory() {
        var connFactory = new ConnectionFactory();
        connFactory.setUri(config.getUri());
        return connFactory;
    }

    private boolean isConnected() {
        return connection != null && connection.isOpen();
    }
}
