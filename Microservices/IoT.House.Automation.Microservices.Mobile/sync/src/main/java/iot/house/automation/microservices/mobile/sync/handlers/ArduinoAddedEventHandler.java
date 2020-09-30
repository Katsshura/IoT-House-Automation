package iot.house.automation.microservices.mobile.sync.handlers;

import iot.house.automation.microservices.mobile.application.interfaces.EventHandler;
import iot.house.automation.microservices.mobile.application.messaging.events.ArduinoAddedEvent;
import iot.house.automation.microservices.mobile.domain.interfaces.ArduinoManagement;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Component
public class ArduinoAddedEventHandler implements EventHandler<ArduinoAddedEvent> {

    private final ArduinoManagement arduinoManagement;

    @Autowired
    public ArduinoAddedEventHandler(ArduinoManagement arduinoManagement) {
        this.arduinoManagement = arduinoManagement;
    }

    @Override
    public Future handle(ArduinoAddedEvent event) {
        if (event == null) return CompletableFuture.completedFuture(true);

        var arduino = event.getArduino();

        try {
            this.arduinoManagement.register(arduino);
            return CompletableFuture.completedFuture(true);
        } catch (Exception e) {
            return CompletableFuture.failedFuture(e);
        }
    }
}
