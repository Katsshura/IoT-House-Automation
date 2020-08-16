package iot.house.automation.microservices.mobile.application.messaging.events;

import lombok.Getter;

import java.util.UUID;

@Getter
public class ArduinoRemovedEvent extends BaseEvent {
    private UUID arduinoUniqueIdentifier;

    public ArduinoRemovedEvent(UUID arduinoUniqueIdentifier) {
        this.arduinoUniqueIdentifier = arduinoUniqueIdentifier;
    }

    @Override
    public String getType() {
        return this.getClass().getSimpleName();
    }
}
