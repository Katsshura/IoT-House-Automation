package iot.house.automation.microservices.mobile.infra.mongo.repositories;

import iot.house.automation.microservices.mobile.infra.mongo.dtos.MobileMongoRepresentation;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface MobileRepository extends MongoRepository<MobileMongoRepresentation, String> {
}
