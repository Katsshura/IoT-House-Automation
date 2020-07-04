using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Api.ViewModel;
using IoT.House.Automation.Microservices.Arduino.Domain.Enums;
using IoT.House.Automation.Microservices.Arduino.Domain.Events;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IoT.House.Automation.Microservices.Arduino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArduino _arduino;

        public ArduinoController(IMapper mapper, IArduino arduino)
        {
            _mapper = mapper;
            _arduino = arduino;
        }

        [HttpPost("register")]
        public ActionResult RegisterNewArduino(ArduinoViewModel arduino)
        {
            try
            {
                var result = MapToDomain(arduino);
                _arduino.Register(result);
                return Ok($"Information about arduino: {result.UniqueIdentifier} - was saved with success!");
            }
            catch (ApplicationException ex)
            {
                return Conflict(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
            
        }

        public ArduinoInfo MapToDomain(ArduinoViewModel model)
        {
            var result = _mapper.Map<ArduinoViewModel, ArduinoInfo>(model);
            result.Events = model.Events.Select(MapEvent);

            return result;
        }

        public Event MapEvent(EventViewModel model)
        {
            return model.EventType.Equals("RangeEvent", StringComparison.InvariantCultureIgnoreCase)
                ? MapEventWithParameters<RangeEvent>(model)
                : _mapper.Map<EventViewModel, Event>(model);
        }

        private TEvent MapEventWithParameters<TEvent>(EventViewModel model)
            where TEvent : Event, new()
        {
            var @event = _mapper.Map<EventViewModel, TEvent>(model);
            var keys = model.Parameters.Keys.ToList();
            keys.ForEach(key => typeof(TEvent).GetProperty(key)?.SetValue(@event, model.Parameters[key]));
            return @event;
        }
    }
}
