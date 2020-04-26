using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Domain.Interfaces
{
    public interface IJwt
    {
        string CreateToken(string username);
        string GetClaimValue(string token);
        bool IsTokenValid(string token);
    }
}
