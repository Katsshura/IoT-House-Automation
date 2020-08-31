package iot.house.automation.microservices.mobile.application.messaging.events;

import lombok.Getter;

import java.util.UUID;

@Getter
public class ArduinoRemovedEvent extends BaseEvent {
    private UUID arduinoIdentifier;

    public ArduinoRemovedEvent(UUID arduinoIdentifier) {
        this.arduinoIdentifier = arduinoIdentifier;
    }

    @Override
    public String getType() {
        return this.getClass().getSimpleName();
    }
}
