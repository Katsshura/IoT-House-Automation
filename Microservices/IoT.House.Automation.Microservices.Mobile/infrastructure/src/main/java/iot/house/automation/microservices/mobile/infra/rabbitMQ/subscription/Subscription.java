package iot.house.automation.microservices.mobile.infra.rabbitMQ.subscription;

import com.google.gson.Gson;
import iot.house.automation.microservices.mobile.application.interfaces.EventHandler;
import iot.house.automation.microservices.mobile.application.messaging.events.BaseEvent;
import lombok.Getter;
import org.springframework.context.ApplicationContext;

import java.lang.reflect.Type;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Getter
public class Subscription {
    private final Type handlerType;
    private final Type eventType;

    private Subscription(Type handlerType, Type eventType) {
        this.handlerType = handlerType;
        this.eventType = eventType;
    }

    public static Subscription New(Type handlerType, Type eventType) {
        return new Subscription(handlerType, eventType);
    }

    public Future handle(String message, ApplicationContext provider) {
        var gson = new Gson();
        var eventData = gson.fromJson(message, eventType);
        var handler = provider.getBean((Class<?>)handlerType);
        try {
            var result = (Future) EventHandler.class.getMethod("handle", BaseEvent.class).invoke(handler, eventData);
            return result;
        } catch (Exception e) {
            return CompletableFuture.failedFuture(e);
        }
    }
}
