using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Domain.Models;

namespace IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events
{
    public class ArduinoAddedEvent : Event
    {
        public ArduinoInfo Arduino { get; }
        public override Type Type => this.GetType();

        public ArduinoAddedEvent(ArduinoInfo arduino)
        {
            Arduino = arduino;
        }
    }
}
