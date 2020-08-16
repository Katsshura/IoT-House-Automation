package iot.house.automation.microservices.mobile.domain.models.arduino.events;

import iot.house.automation.microservices.mobile.domain.enums.EventInputType;
import lombok.Getter;

@Getter
public class Event {
    private String name;
    private String description;
    private EventInputType expectedInputType;
    private String type;

    public Event() {
        type = this.getClass().getSimpleName();
    }
}
