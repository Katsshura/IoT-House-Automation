package iot.house.automation.microservices.mobile.application.messaging.events;

import lombok.Getter;

import java.time.Instant;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.UUID;

@Getter
public abstract class BaseEvent {
    private final UUID id;
    private final Date createdAt;

    public abstract String getType();

    protected BaseEvent(){
        id = UUID.randomUUID();
        createdAt = Date.from(Instant.now());
    }
}
