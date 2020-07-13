using IoT.House.Automation.Microservices.Arduino.Domain.Models;
using System;
using System.Collections.Generic;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Interfaces
{
    public interface IArduino
    {
        void Register(ArduinoInfo arduino);
        ArduinoInfo GetArduino(Guid identification);
        IEnumerable<ArduinoInfo> GetArduinos();
        bool Remove(Guid identification);
    }
}
