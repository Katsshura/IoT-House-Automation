package iot.house.automation.microservices.mobile.application.interfaces;

import iot.house.automation.microservices.mobile.application.messaging.events.BaseEvent;

public interface EventBus {
    void publish(BaseEvent event);

    <TEvent extends BaseEvent, TEventHandler extends EventHandler> void subscribe(Class<TEvent> eventType, Class<TEventHandler> handlerType);

    <TEvent extends BaseEvent, TEventHandler extends EventHandler> void unsubscribe(Class<TEvent> eventType, Class<TEventHandler> handlerType);
}
