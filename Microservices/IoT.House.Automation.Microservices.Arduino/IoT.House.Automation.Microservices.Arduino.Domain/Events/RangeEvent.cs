﻿using System;

namespace IoT.House.Automation.Microservices.Arduino.Domain.Events
{
    public class RangeEvent : Event
    {
        public object MinValue { get; set; }
        public object MaxValue { get; set; }

        public override Type Type => this.GetType();

        public override bool IsValid()
        {
            return base.IsValid()
                   && MinValue != null
                   && MaxValue != null
                   && MinValue.GetType() == MaxValue.GetType();
        }
    }
}
