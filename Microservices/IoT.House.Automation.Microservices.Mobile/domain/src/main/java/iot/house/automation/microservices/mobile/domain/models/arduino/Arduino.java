package iot.house.automation.microservices.mobile.domain.models.arduino;

import iot.house.automation.microservices.mobile.domain.models.arduino.events.Event;
import lombok.Getter;

import java.util.List;
import java.util.UUID;

@Getter
public class Arduino {
    private UUID uniqueIdentifier;
    private String name;
    private List<Event> events;
}