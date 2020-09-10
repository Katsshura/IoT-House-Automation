package iot.house.automation.microservices.mobile.infra.mongo.dtos;

import lombok.Getter;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "mobiles")
@Getter
public class MobileMongoRepresentation {
    @Id
    private String username;
    private String firebaseToken;

    public MobileMongoRepresentation(String username, String firebaseToken) {
        this.username = username;
        this.firebaseToken = firebaseToken;
    }
}
