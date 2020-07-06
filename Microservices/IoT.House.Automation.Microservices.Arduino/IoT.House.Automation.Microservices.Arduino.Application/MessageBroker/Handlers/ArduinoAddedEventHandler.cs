using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;

namespace IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Handlers
{
    public class ArduinoAddedEventHandler : IEventHandler<ArduinoAddedEvent>
    {
        public Task Handle(ArduinoAddedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
