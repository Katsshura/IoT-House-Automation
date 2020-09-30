package iot.house.automation.microservices.mobile.infra.mongo.dtos;

import iot.house.automation.microservices.mobile.domain.models.arduino.events.Event;
import lombok.Getter;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.List;

@Document(collection = "arduinos")
@Getter
public class ArduinoMongoRepresentation {
    @Id
    private final String uniqueIdentifier;
    private final String name;
    private final List<Event> events;

    public ArduinoMongoRepresentation(String uniqueIdentifier, String name, List<Event> events) {
        this.uniqueIdentifier = uniqueIdentifier;
        this.name = name;
        this.events = events;
    }
}
