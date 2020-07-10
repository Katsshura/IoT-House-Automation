using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.Util;
using Newtonsoft.Json;

namespace IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Subscription
{
    public class Subscription
    {
        public Type HandlerType { get; }
        public Type EventType { get; }

        private Subscription(Type handlerType, Type eventType)
        {
            HandlerType = handlerType;
            EventType = eventType;
        }

        public async Task Handle(string message, IServiceProvider provider)
        {
            var settings = NewtonsoftJsonUtil.UseCustomNewtonsoftSettings();
            var eventData = JsonConvert.DeserializeObject(message, EventType, settings);
            var handler = provider.GetService(HandlerType);

            await (Task)HandlerType.GetMethod("Handle")
                .Invoke(handler, new[] { eventData });
        }

        public static Subscription New(Type handlerType, Type eventType) => new Subscription(handlerType, eventType);
    }
}
