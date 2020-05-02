using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Domain.Interfaces
{
    public interface IAuthRepository
    {
        bool IsCredentialsValid(Login login);
        DataSet GetUserInformation(string username);
    }
}
