package iot.house.automation.microservices.mobile.domain.models.arduino.events;

import lombok.Getter;

@Getter
public class RangeEvent extends Event {
    private Object minValue;
    private Object maxValue;
}
