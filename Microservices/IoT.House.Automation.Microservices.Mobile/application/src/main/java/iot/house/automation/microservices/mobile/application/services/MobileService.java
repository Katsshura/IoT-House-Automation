package iot.house.automation.microservices.mobile.application.services;

import iot.house.automation.microservices.mobile.application.interfaces.NoSqlDatabase;
import iot.house.automation.microservices.mobile.domain.interfaces.MobileManagement;
import iot.house.automation.microservices.mobile.domain.models.mobile.Mobile;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public class MobileService implements MobileManagement {

    private final NoSqlDatabase<Mobile, String> repository;

    public MobileService(NoSqlDatabase<Mobile, String> repository) {
        this.repository = repository;
    }

    @Override
    public void register(Mobile mobile) {
        var exists = this.isAlreadyRegistered(mobile.getUsername());

        if(exists) {
            this.delete(mobile.getUsername());
        }

        repository.save(mobile);
    }

    @Override
    public Mobile getMobile(String id) {
        return repository.find(id);
    }

    @Override
    public List<Mobile> getAll() {
        return repository.findAll();
    }

    @Override
    public void delete(String id) {
        repository.delete(id);
    }

    private boolean isAlreadyRegistered(String id) {
        var res = getMobile(id);
        return res != null;
    }
}
