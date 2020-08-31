package iot.house.automation.microservices.mobile.application.interfaces;

import iot.house.automation.microservices.mobile.application.messaging.events.BaseEvent;

import java.util.concurrent.Future;

public interface EventHandler<TEvent extends BaseEvent> {
    Future handle(TEvent event);
}
