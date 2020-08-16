package iot.house.automation.microservices.mobile.application.messaging.events;

import iot.house.automation.microservices.mobile.domain.models.arduino.Arduino;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ArduinoAddedEvent extends BaseEvent {
    private Arduino arduino;

    public ArduinoAddedEvent(Arduino arduino) {
        this.arduino = arduino;
    }

    @Override
    public String getType() {
        return this.getClass().getSimpleName();
    }
}
