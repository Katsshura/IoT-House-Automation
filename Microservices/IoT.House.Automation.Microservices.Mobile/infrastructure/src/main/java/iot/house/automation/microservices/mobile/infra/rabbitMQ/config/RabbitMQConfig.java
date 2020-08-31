package iot.house.automation.microservices.mobile.infra.rabbitMQ.config;

import lombok.Getter;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

@Component
@Getter
public class RabbitMQConfig {
    private final String uri;
    private final String publishExchange;
    private final String listenerExchange;
    private final String exchangeType;
    private int retryCount;

    public RabbitMQConfig(@Value("${spring.rabbitmq.uri}") String uri,
                          @Value("${spring.rabbitmq.publisherExchange}") String publishExchange,
                          @Value("${spring.rabbitmq.listenerExchange}") String listenerExchange,
                          @Value("${spring.rabbitmq.exchangeType}") String exchangeType,
                          @Value("${spring.rabbitmq.retryCount}") int retryCount)
    {
        this.uri = uri;
        this.publishExchange = publishExchange;
        this.listenerExchange = listenerExchange;
        this.exchangeType = exchangeType;
        this.retryCount = retryCount;
    }
}
