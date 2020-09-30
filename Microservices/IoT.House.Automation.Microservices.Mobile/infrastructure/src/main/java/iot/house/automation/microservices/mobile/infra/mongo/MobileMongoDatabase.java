package iot.house.automation.microservices.mobile.infra.mongo;

import iot.house.automation.microservices.mobile.application.interfaces.NoSqlDatabase;
import iot.house.automation.microservices.mobile.domain.models.mobile.Mobile;
import iot.house.automation.microservices.mobile.infra.mongo.dtos.MobileMongoRepresentation;
import iot.house.automation.microservices.mobile.infra.mongo.repositories.MobileRepository;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.stream.Collectors;

@Component
public class MobileMongoDatabase implements NoSqlDatabase<Mobile, String> {

    private final MobileRepository repository;

    public MobileMongoDatabase(MobileRepository repository) {
        this.repository = repository;
    }

    @Override
    public void save(Mobile mobile) {
        var dto = new MobileMongoRepresentation(mobile.getUsername(), mobile.getFirebaseToken());
        repository.save(dto);
    }

    @Override
    public Mobile find(String id) {
        var result = repository.findById(id);

        if(result.isEmpty()) return null;

        var res = result.get();
        return new Mobile(res.getUsername(), res.getFirebaseToken());
    }

    @Override
    public List<Mobile> findAll() {
        var result = repository.findAll();
        var res = result.stream().map(dto -> new Mobile(dto.getUsername(), dto.getFirebaseToken()));
        return res.collect(Collectors.toList());
    }

    @Override
    public void delete(String id) {
        repository.deleteById(id);
    }
}
