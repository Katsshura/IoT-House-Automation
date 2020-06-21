using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Api.ViewModel;
using IoT.House.Automation.Microservices.Arduino.Domain.Enums;
using IoT.House.Automation.Microservices.Arduino.Domain.Events;
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

        public ArduinoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("register")]
        public ActionResult RegisterNewArduino(ArduinoViewModel arduino)
        {
            var res = MapToDomain(arduino);

            return new JsonResult("OK");
        }

        public ArduinoInfo MapToDomain(ArduinoViewModel model)
        {
            var result = new ArduinoInfo
            {
                Name = model.Name,
                IP = IPAddress.Parse(model.IP),
                Port = model.Port,
                UniqueIdentifier = new Guid(model.UniqueIdentifier),
                Events = model.Events.Select(MapEvent)
            };

            return result;
        }

        public Event MapEvent(EventViewModel model)
        {
            if (model.EventType.Equals("RangeEvent", StringComparison.InvariantCultureIgnoreCase))
            {
                return new RangeEvent
                {
                    Name = model.Name,
                    Description = model.Description,
                    ExpectedInputType = Enum.Parse<EventInputType>(model.ExpectedInputType, true),
                    MaxValue = model.Parameters["MaxValue"],
                    MinValue = model.Parameters["MinValue"]
                };
            }

            return new Event
            {
                Name = model.Name,
                Description = model.Description,
                ExpectedInputType = Enum.Parse<EventInputType>(model.ExpectedInputType, true)
            };
        }
    }
}
