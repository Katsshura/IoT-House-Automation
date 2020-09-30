package iot.house.automation.microservices.mobile.domain.interfaces;

import java.util.UUID;

public interface EventEmitter {
    void emit(UUID arduinoIdentifier, String eventName, Object eventValue);
}
