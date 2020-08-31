package iot.house.automation.microservices.mobile.sync;

import iot.house.automation.microservices.mobile.application.interfaces.EventBus;
import iot.house.automation.microservices.mobile.application.messaging.events.ArduinoAddedEvent;
import iot.house.automation.microservices.mobile.sync.handlers.ArduinoAddedEventHandler;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.mongodb.repository.config.EnableMongoRepositories;

@EnableMongoRepositories(basePackages = "iot.house.automation.microservices")
@SpringBootApplication(scanBasePackages = "iot.house.automation.microservices")
public class SyncApplication {

    public static void main(String[] args) {
        var context = SpringApplication.run(SyncApplication.class, args);
        var bus = context.getBean(EventBus.class);
        bus.subscribe(ArduinoAddedEvent.class, ArduinoAddedEventHandler.class);
    }

}
