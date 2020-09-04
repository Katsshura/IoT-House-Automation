using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Domain.Models;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Interfaces
{
    public interface IEventTrigger
    {
        void Emit(ArduinoInfo to, string eventName, object eventValue);
    }
}
