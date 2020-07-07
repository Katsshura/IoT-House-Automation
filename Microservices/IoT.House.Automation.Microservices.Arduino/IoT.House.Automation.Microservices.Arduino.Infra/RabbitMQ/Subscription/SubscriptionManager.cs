using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;

namespace IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Subscription
{
    public class SubscriptionManager
    {
        private readonly IDictionary<string, IList<Subscription>> _handlers
            = new Dictionary<string, IList<Subscription>>();

        public bool IsEmpty => !_handlers.Keys.Any();

        public IEnumerable<string> EventNames => _handlers.Keys;

        public event EventHandler<string> OnEventRemoved;
        public event EventHandler<string> OnEventAdded;

        public void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            AddSubscription(
                typeof(TEventHandler),
                typeof(TEvent).Name,
                typeof(TEvent)
                );
        }
        
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public IEnumerable<Subscription> GetHandlersForEvent<TEvent>()
            where TEvent : Event
        {
            var key = typeof(TEvent).Name;
            return GetHandlersForEvent(key);
        }

        public IEnumerable<Subscription> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public Type GetEventTypeByName(string eventName) => _handlers[eventName]?.FirstOrDefault()?.EventType;

        public void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {

            var eventName = typeof(TEvent).Name;
            var handlerToRemove = FindSubscriptionToRemove(eventName, typeof(TEventHandler));
            RemoveSubscription(eventName, handlerToRemove);
        }

        private void AddSubscription(Type handlerType, string eventName, Type eventType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<Subscription>());
                OnEventAdded?.Invoke(this, eventName);
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(Subscription.New(
                handlerType, eventType)
                );
        }

        private Subscription FindSubscriptionToRemove(string eventName, Type eventHandler)
        {
            return !HasSubscriptionsForEvent(eventName) 
                ? null 
                : _handlers[eventName].SingleOrDefault(s => s.HandlerType == eventHandler);
        }

        private void RemoveSubscription(
            string eventName,
            Subscription subsToRemove
            )
        {
            if (subsToRemove == null) return;
            _handlers[eventName].Remove(subsToRemove);

            if (_handlers[eventName].Any()) return;

            _handlers.Remove(eventName);
            OnEventRemoved?.Invoke(this, eventName);
        }
    }
}
