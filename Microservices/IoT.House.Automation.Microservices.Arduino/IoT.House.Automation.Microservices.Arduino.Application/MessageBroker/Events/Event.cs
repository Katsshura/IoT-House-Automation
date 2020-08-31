using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events
{
    public abstract class Event
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public abstract string Type { get; }

        protected Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
