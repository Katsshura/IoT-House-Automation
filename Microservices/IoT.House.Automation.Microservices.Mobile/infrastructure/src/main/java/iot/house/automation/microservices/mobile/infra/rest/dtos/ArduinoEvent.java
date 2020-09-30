package iot.house.automation.microservices.mobile.infra.rest.dtos;

import lombok.Getter;

@Getter
public class ArduinoEvent {
    private String arduinoIdentifier;
    private String eventTargetName;
    private Object eventTargetValue;

    public ArduinoEvent(String arduinoIdentifier, String eventTargetName, Object eventTargetValue) {
        this.arduinoIdentifier = arduinoIdentifier;
        this.eventTargetName = eventTargetName;
        this.eventTargetValue = eventTargetValue;
    }
}
