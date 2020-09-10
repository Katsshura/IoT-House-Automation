package iot.house.automation.microservices.mobile.domain.models.mobile;

import lombok.Getter;

@Getter
public class Mobile {
    private final String username;
    private final String firebaseToken;

    public Mobile(String username, String firebaseToken) {
        this.username = username;
        this.firebaseToken = firebaseToken;
    }

    public boolean isValid() {
        return this.username != null
                && this.firebaseToken != null;
    }
}
