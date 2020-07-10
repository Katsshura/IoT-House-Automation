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
            var ran = new Random().Next(1, 100);

            return ran < 50 ? Task.CompletedTask : Task.FromException(new NotImplementedException());
        }
    }
}
