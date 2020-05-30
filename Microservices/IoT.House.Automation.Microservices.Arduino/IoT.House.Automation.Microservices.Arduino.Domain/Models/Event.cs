using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Domain.Enums;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Models
{
    public class Event
    {
        public string Name { get; set; }
        public EventInputType InputType { get; set; }
        public IEnumerable<object> Values { get; set; }
    }
}
