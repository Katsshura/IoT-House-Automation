using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Domain.Interfaces
{
    public interface IAuth
    {
        string Signin(Login login);
        User GetSignedUser(string token);
    }
}
