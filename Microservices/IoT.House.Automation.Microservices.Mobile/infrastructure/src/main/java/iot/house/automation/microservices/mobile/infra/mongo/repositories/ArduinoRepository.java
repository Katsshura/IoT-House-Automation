package iot.house.automation.microservices.mobile.infra.mongo.repositories;

import iot.house.automation.microservices.mobile.infra.mongo.dtos.ArduinoMongoRepresentation;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ArduinoRepository extends MongoRepository<ArduinoMongoRepresentation, String> {
}
