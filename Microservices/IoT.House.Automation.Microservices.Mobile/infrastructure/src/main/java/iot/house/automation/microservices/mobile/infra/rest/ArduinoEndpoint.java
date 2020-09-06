package iot.house.automation.microservices.mobile.infra.rest;

import com.google.gson.Gson;
import iot.house.automation.microservices.mobile.domain.interfaces.EventEmitter;
import iot.house.automation.microservices.mobile.infra.rest.dtos.ArduinoEvent;
import lombok.SneakyThrows;
import org.jsoup.Connection;
import org.jsoup.Jsoup;
import org.springframework.stereotype.Component;

import java.util.UUID;

@Component
public class ArduinoEndpoint implements EventEmitter {

    private final String base_url = "http://localhost:51565/api/mobile";
    private final Gson gson;

    public ArduinoEndpoint() {
        gson = new Gson();
    }

    @SneakyThrows
    @Override
    public void emit(UUID arduinoIdentifier, String eventName, Object eventValue) {
        var body = new ArduinoEvent(arduinoIdentifier.toString(), eventName, eventValue);
        var url = base_url + "/trigger";
        var jsonBody = getJsonBody(body);
        var request = getPostConnection(url, jsonBody);

        request.execute();
    }

    private String getJsonBody(Object body) {
        return gson.toJson(body);
    }

    private Connection getPostConnection(String url, String body) {
        return Jsoup.connect(url)
                .method(Connection.Method.POST)
                .header("Content-Type", "application/json")
                .requestBody(body);
    }
}
