using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Domain.Events;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Models
{
    public class ArduinoInfo
    {
        public Guid UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
