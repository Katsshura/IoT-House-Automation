using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IoT.House.Automation.Microservices.Auth.Api
{
    public class LoginViewModel
    {
        [FromBody]
        public string Username { get; set; }
        [FromBody]
        public string Password { get; set; }
    }
}
