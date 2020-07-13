using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;

namespace IoT.House.Automation.Microservices.Arduino.Application.Interfaces
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
}
