using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Models
{
    public class Arduino
    {
        public Guid UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
