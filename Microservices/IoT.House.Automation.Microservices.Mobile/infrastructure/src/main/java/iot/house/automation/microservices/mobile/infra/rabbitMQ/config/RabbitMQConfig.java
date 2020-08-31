package iot.house.automation.microservices.mobile.infra.rabbitMQ.config;

import lombok.Getter;
import org.springframework.stereotype.Component;

@Component
@Getter
public class RabbitMQConfig {
    private String uri;
    private String publishExchange;
    private String listenerExchange;
    private String exchangeType;
    private int retryCount;

    public RabbitMQConfig() {
        uri = "";
        listenerExchange = "Arduino";
        publishExchange = "Mobile";
        exchangeType = "direct";
        retryCount = 3;
    }
}
