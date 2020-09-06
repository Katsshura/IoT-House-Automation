package iot.house.automation.microservices.mobile.api.dtos;

import lombok.Getter;
import org.springframework.util.StringUtils;

@Getter
public class EventTrigger {
    private String arduinoIdentifier;
    private String eventTargetName;
    private Object eventTargetValue;

    public boolean isValid() {
        return arduinoIdentifier != null && !StringUtils.isEmpty(arduinoIdentifier)
                && eventTargetName != null && !StringUtils.isEmpty(eventTargetName)
                && eventTargetName != null;
    }
}
