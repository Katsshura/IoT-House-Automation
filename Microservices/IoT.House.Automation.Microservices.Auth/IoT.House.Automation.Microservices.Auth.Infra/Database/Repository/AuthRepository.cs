using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Infra.Database.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public bool IsCredentialsValid(Login login)
        {
            throw new NotImplementedException();
        }

        public DataSet GetUserInformation(string username)
        {
            throw new NotImplementedException();
        }
    }
}
