using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IoT.House.Automation.Microservices.Arduino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        [HttpPost("register")]
        public ActionResult RegisterNewArduino(ArduinoViewModel arduino)
        {
            return new JsonResult("OK");
        }
    }
}
