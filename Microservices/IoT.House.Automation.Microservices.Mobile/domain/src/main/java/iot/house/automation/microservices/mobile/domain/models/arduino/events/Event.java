package iot.house.automation.microservices.mobile.domain.models.arduino.events;

import lombok.Getter;

@Getter
public class Event {
    private String name;
    private String description;
    private String expectedInputType;
    private final String type;

    public Event() {
        type = this.getClass().getSimpleName();
    }
}
