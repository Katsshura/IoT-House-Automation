using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events
{
    public class ArduinoRemovedEvent : Event
    {
        public Guid ArduinoIdentifier { get; set; }
        public override string Type => this.GetType().Name;

        public ArduinoRemovedEvent(Guid arduinoIdentifier)
        {
            ArduinoIdentifier = arduinoIdentifier;
        }
    }
}
