package iot.house.automation.microservices.mobile.infra.rabbitMQ.subscription;

import com.sun.jdi.request.DuplicateRequestException;
import iot.house.automation.microservices.mobile.application.interfaces.EventHandler;
import iot.house.automation.microservices.mobile.application.messaging.events.BaseEvent;
import lombok.Setter;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;

public class SubscriptionManager {
    private final Map<String, List<Subscription>> handlers;

    @Setter
    private Consumer<String> onEventRemoved;
    @Setter
    private Consumer<String> onEventAdded;

    public SubscriptionManager() {
        handlers = new HashMap<>();
    }

    public boolean isEmpty() {
        return handlers.isEmpty();
    }

    public boolean hasSubscriptionForEvent(String eventName) {
        return handlers.containsKey(eventName);
    }

    public List<Subscription> getHandlersForEvent(String eventName) {
        return handlers.get(eventName);
    }

    public <TEvent extends BaseEvent, TEventHandler extends EventHandler> void addSubscription(Class<TEvent> eventType, Class<TEventHandler> handlerType) {
        addSubscription(eventType, eventType.getSimpleName(), handlerType);
    }

    public <TEvent extends BaseEvent, TEventHandler extends EventHandler> void removeSubscription(Class<TEvent> eventType, Class<TEventHandler> handlerType) {
        var eventName = eventType.getSimpleName();
        var handlerToRemove = findSubscriptionToRemove(eventName, handlerType);
        removeSubscription(eventName, handlerToRemove);
    }

    private <TEventHandler extends EventHandler> Subscription findSubscriptionToRemove(String eventName, Class<TEventHandler> handlerType) {
        return !hasSubscriptionForEvent(eventName)
                ? null
                : handlers.get(eventName).stream().filter(s -> s.getHandlerType() == handlerType).findFirst().orElse(null);
    }

    private <TEvent extends BaseEvent, TEventHandler extends EventHandler> void addSubscription(Class<TEvent> eventType, String eventName, Class<TEventHandler> handlerType) {
        if(!hasSubscriptionForEvent(eventName)){
            handlers.put(eventName, new ArrayList<>());
        }

        var handler = handlers.get(eventName);

        if(handler.stream().anyMatch(s -> s.getHandlerType() == handlerType)){
            throw new DuplicateRequestException(String.format("Handler Type %s already registered for '%s'", handlerType.getSimpleName(), eventName));
        }

        handler.add(Subscription.New(handlerType, eventType));
        onEventAdded.accept(eventName);
    }

    private void removeSubscription(String eventName, Subscription subscription) {
        if(subscription == null) return;
        handlers.get(eventName).remove(subscription);

        if(handlers.get(eventName).size() > 0) return;

        handlers.remove(eventName);
        onEventRemoved.accept(eventName);
    }
}
