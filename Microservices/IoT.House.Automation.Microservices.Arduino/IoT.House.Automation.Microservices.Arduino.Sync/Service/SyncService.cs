using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;
using IoT.House.Automation.Microservices.Arduino.Sync.Handlers;

namespace IoT.House.Automation.Microservices.Arduino.Sync.Service
{
    public class SyncService : ISync
    {
        private readonly IEventBus _eventBus;

        public SyncService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            SubscribeToBus();
        }

        private void SubscribeToBus()
        {
            _eventBus.Subscribe<ArduinoRemovedEvent, ArduinoRemovedEventHandler>();
        }
    }
}
