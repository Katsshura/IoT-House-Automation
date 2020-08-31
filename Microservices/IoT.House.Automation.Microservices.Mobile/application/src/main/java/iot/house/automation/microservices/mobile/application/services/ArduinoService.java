package iot.house.automation.microservices.mobile.application.services;

import iot.house.automation.microservices.mobile.application.interfaces.NoSqlDatabase;
import iot.house.automation.microservices.mobile.domain.interfaces.ArduinoManagement;
import iot.house.automation.microservices.mobile.domain.models.arduino.Arduino;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.UUID;

@Component
public class ArduinoService implements ArduinoManagement {

    private final NoSqlDatabase<Arduino, UUID> repository;

    public ArduinoService(NoSqlDatabase<Arduino, UUID> repository) {
        this.repository = repository;
    }

    @Override
    public void register(Arduino arduino) {
        var exists = this.isAlreadyRegistered(arduino.getUniqueIdentifier());

        if(exists) {
            this.delete(arduino.getUniqueIdentifier());
        }

        if(!arduino.isValid()) return;

        repository.save(arduino);
    }

    @Override
    public Arduino getArduino(UUID uniqueIdentifier) {
        return repository.find(uniqueIdentifier);
    }

    @Override
    public List<Arduino> getArduinos() {
        return repository.findAll();
    }

    @Override
    public void delete(UUID uniqueIdentifier) {
        repository.delete(uniqueIdentifier);
    }

    private boolean isAlreadyRegistered(UUID uuid) {
        var res = getArduino(uuid);
        return res != null && res.isValid();
    }
}
