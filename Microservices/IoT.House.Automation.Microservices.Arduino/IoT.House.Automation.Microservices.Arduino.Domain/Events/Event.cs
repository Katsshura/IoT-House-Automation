using System;
using IoT.House.Automation.Microservices.Arduino.Domain.Enums;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Events
{
    public class Event
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EventInputType ExpectedInputType { get; set; }

        public virtual Type Type => this.GetType();


        public virtual bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description);
        }
    }
}
