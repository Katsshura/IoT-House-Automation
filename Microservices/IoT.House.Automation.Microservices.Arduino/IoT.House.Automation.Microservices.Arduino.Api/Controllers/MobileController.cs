using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Api.ViewModel;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT.House.Automation.Microservices.Arduino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IArduino _arduino;
        private readonly IEventTrigger _trigger;

        public MobileController(IArduino arduino, IEventTrigger trigger)
        {
            _arduino = arduino;
            _trigger = trigger;
        }

        [HttpPost("trigger")]
        public ActionResult TriggerEventOnArduinoDevice(EventTriggerViewModel trigger)
        {
            try
            {
                var arduino = _arduino.GetArduino(Guid.Parse(trigger.ArduinoIdentifier));

                if (arduino == null)
                {
                    return NotFound("The requested arduino was not found in our database");
                }

                _trigger.Emit(arduino, trigger.EventTargetName, trigger.EventTargetValue);

                return Ok("Event was sent with success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Our server had a problem to process your request, please try again later");
            }
        }
    }
}
