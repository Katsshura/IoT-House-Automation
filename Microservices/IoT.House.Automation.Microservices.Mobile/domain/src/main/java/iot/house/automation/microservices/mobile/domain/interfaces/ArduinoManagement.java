package iot.house.automation.microservices.mobile.domain.interfaces;

import iot.house.automation.microservices.mobile.domain.models.arduino.Arduino;

import java.util.List;
import java.util.UUID;

public interface ArduinoManagement {
    void register(Arduino arduino);
    Arduino getArduino(UUID uniqueIdentifier);
    List<Arduino> getArduinos();
    void delete(UUID uniqueIdentifier);
}
