package iot.house.automation.microservices.mobile.sync.handlers;

import iot.house.automation.microservices.mobile.application.interfaces.EventHandler;
import iot.house.automation.microservices.mobile.application.messaging.events.ArduinoAddedEvent;
import org.springframework.stereotype.Component;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Component
public class ArduinoAddedEventHandler implements EventHandler<ArduinoAddedEvent> {

    @Override
    public Future handle(ArduinoAddedEvent event) {
        System.out.println(event);
        return CompletableFuture.completedFuture(true);
    }
}
