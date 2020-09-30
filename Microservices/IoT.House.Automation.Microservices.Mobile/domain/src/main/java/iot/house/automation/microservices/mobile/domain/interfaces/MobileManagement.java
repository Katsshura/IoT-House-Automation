package iot.house.automation.microservices.mobile.domain.interfaces;

import iot.house.automation.microservices.mobile.domain.models.mobile.Mobile;

import java.util.List;

public interface MobileManagement {
    void register(Mobile mobile);
    Mobile getMobile(String id);
    List<Mobile> getAll();
    void delete(String id);
}
