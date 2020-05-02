using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IoT.House.Automation.Microservices.Auth.Api
{
    public class LoginViewModel
    {
        [FromBody]
        [Required]
        public string Username { get; set; }
        [FromBody]
        [Required]
        public string Password { get; set; }
    }
}
