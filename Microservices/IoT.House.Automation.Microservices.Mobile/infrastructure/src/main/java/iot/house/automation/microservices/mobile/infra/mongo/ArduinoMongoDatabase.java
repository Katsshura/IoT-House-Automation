package iot.house.automation.microservices.mobile.infra.mongo;

import iot.house.automation.microservices.mobile.application.interfaces.NoSqlDatabase;
import iot.house.automation.microservices.mobile.domain.models.arduino.Arduino;
import iot.house.automation.microservices.mobile.infra.mongo.dtos.ArduinoMongoRepresentation;
import iot.house.automation.microservices.mobile.infra.mongo.repositories.ArduinoRepository;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@Component
public class ArduinoMongoDatabase implements NoSqlDatabase<Arduino, UUID> {

    private final ArduinoRepository repository;

    public ArduinoMongoDatabase(ArduinoRepository repository) {
        this.repository = repository;
    }

    @Override
    public void save(Arduino arduino) {
        var dto = new ArduinoMongoRepresentation(arduino.getUniqueIdentifier().toString(),arduino.getName(), arduino.getEvents());
        repository.save(dto);
    }

    @Override
    public Arduino find(UUID uuid) {
        var result = repository.findById(uuid.toString());

        if(result.isEmpty()) return null;

        var res = result.get();
        var ard = new Arduino(UUID.fromString(res.getUniqueIdentifier()), res.getName(), res.getEvents());
        return ard;
    }

    @Override
    public List<Arduino> findAll() {
        var res = repository.findAll();
        var result = res.stream().map(dto -> new Arduino(UUID.fromString(dto.getUniqueIdentifier()), dto.getName(), dto.getEvents()) );
        return result.collect(Collectors.toList());
    }

    @Override
    public void delete(UUID uuid) {
        repository.deleteById(uuid.toString());
    }
}
