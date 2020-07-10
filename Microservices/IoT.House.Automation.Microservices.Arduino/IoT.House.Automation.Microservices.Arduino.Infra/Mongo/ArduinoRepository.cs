using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Handlers;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Models;
using MongoDB.Driver;

namespace IoT.House.Automation.Microservices.Arduino.Infra.Mongo
{
    public class ArduinoRepository : IArduino
    {
        private readonly IMongoCollection<ArduinoInfo> _collection;
        private readonly IEventBus _eventBus;

        public ArduinoRepository(IMongoClient client, IEventBus eventBus)
        {
            _eventBus = eventBus;

            var database = client.GetDatabase("arduino");
            _collection = database.GetCollection<ArduinoInfo>("arduinos");
        }

        public void Register(ArduinoInfo arduino)
        {
            var exists = GetArduino(arduino.UniqueIdentifier) != null;

            if (exists) throw new ApplicationException("Arduino already registered in database");

            _collection.InsertOne(arduino);
            _eventBus.Subscribe<ArduinoAddedEvent, ArduinoAddedEventHandler>();

            _eventBus.Publish(new ArduinoAddedEvent(arduino));
        }

        public ArduinoInfo GetArduino(Guid identification)
        {
            return _collection.Find(doc => doc.UniqueIdentifier == identification).FirstOrDefault();
        }

        public IEnumerable<ArduinoInfo> GetArduinos()
        {
            return _collection.Find(FilterDefinition<ArduinoInfo>.Empty).ToEnumerable();
        }

        public bool Remove(Guid identification)
        {
            var result = _collection.DeleteOne(doc => doc.UniqueIdentifier == identification);
            return result.IsAcknowledged;
        }
    }
}
