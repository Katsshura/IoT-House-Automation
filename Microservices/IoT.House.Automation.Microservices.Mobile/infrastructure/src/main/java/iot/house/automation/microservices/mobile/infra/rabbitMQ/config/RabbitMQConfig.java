package iot.house.automation.microservices.mobile.infra.rabbitMQ.config;

import lombok.Getter;
import org.springframework.stereotype.Component;

@Component
@Getter
public class RabbitMQConfig {
    private final String uri;
    private final String publishExchange;
    private final String listenerExchange;
    private final String exchangeType;
    private final int retryCount;

    public RabbitMQConfig() {
        uri = "";
        listenerExchange = "Arduino";
        publishExchange = "Mobile";
        exchangeType = "direct";
        retryCount = 3;
    }
}
