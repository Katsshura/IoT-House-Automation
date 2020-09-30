package iot.house.automation.microservices.mobile.api.controllers;

import iot.house.automation.microservices.mobile.api.dtos.EventTrigger;
import iot.house.automation.microservices.mobile.domain.interfaces.ArduinoManagement;
import iot.house.automation.microservices.mobile.domain.interfaces.EventEmitter;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/arduino")
public class ArduinoController {

    private final ArduinoManagement arduinoManagement;
    private final EventEmitter eventEmitter;

    public ArduinoController(ArduinoManagement arduinoManagement, EventEmitter eventEmitter) {
        this.arduinoManagement = arduinoManagement;
        this.eventEmitter = eventEmitter;
    }

    @RequestMapping(value = "/{uuid}", method = RequestMethod.GET)
    private ResponseEntity getArduino(@PathVariable String uuid) {

        if (!isUUIDValid(uuid)) {
            return new ResponseEntity("You must pass a valid UUID", HttpStatus.BAD_REQUEST);
        }

        var result = arduinoManagement.getArduino(UUID.fromString(uuid));

        return getResponseEntity(result);
    }

    @RequestMapping(value = "/all", method = RequestMethod.GET)
    private ResponseEntity getArduinos() {
        var result = arduinoManagement.getArduinos();
        return getResponseEntity(result);
    }

    @RequestMapping(value = "/delete/{uuid}", method = RequestMethod.DELETE)
    private ResponseEntity deleteArduino(@PathVariable String uuid) {

        if (!isUUIDValid(uuid)) {
            return new ResponseEntity("You must pass a valid UUID", HttpStatus.BAD_REQUEST);
        }

        try {
            arduinoManagement.delete(UUID.fromString(uuid));
            return new ResponseEntity(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @RequestMapping(value = "/trigger", method = RequestMethod.POST)
    private ResponseEntity triggerEvent(@RequestBody EventTrigger eventTrigger) {

        if (!eventTrigger.isValid() || !isUUIDValid(eventTrigger.getArduinoIdentifier())) {
            return new ResponseEntity("Body is invalid", HttpStatus.BAD_REQUEST);
        }

        eventEmitter.emit(
                UUID.fromString(eventTrigger.getArduinoIdentifier()),
                eventTrigger.getEventTargetName(),
                eventTrigger.getEventTargetValue());

        return new ResponseEntity("Event was emitted with success", HttpStatus.OK);
    }

    private ResponseEntity getResponseEntity(Object res) {
        return res == null
                ? new ResponseEntity(HttpStatus.NO_CONTENT)
                : new ResponseEntity(res, HttpStatus.OK);
    }

    private boolean isUUIDValid(String uuid) {
        return uuid.matches("^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$");
    }
}
