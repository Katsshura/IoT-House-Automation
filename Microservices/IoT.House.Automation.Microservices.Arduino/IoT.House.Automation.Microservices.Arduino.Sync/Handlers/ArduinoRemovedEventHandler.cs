using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;

namespace IoT.House.Automation.Microservices.Arduino.Sync.Handlers
{
    public class ArduinoRemovedEventHandler : IEventHandler<ArduinoRemovedEvent>
    {
        private readonly IArduino _arduino;

        public ArduinoRemovedEventHandler(IArduino arduino)
        {
            _arduino = arduino;
        }

        public Task Handle(ArduinoRemovedEvent @event)
        {
            try
            {
                _arduino.Remove(@event.ArduinoIdentifier);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    }
}
