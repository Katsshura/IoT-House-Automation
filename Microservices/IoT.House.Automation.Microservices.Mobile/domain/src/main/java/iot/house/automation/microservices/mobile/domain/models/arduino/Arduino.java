package iot.house.automation.microservices.mobile.domain.models.arduino;

import iot.house.automation.microservices.mobile.domain.models.arduino.events.Event;
import lombok.Getter;

import java.util.List;
import java.util.UUID;

@Getter
public class Arduino {
    private final UUID uniqueIdentifier;
    private final String name;
    private final List<Event> events;

    public Arduino(UUID uniqueIdentifier, String name, List<Event> events) {
        this.uniqueIdentifier = uniqueIdentifier;
        this.name = name;
        this.events = events;
    }

    public boolean isValid(){
        return this.uniqueIdentifier != null
                && this.name != null
                && this.events != null
                && this.events.size() > 0;
    }
}