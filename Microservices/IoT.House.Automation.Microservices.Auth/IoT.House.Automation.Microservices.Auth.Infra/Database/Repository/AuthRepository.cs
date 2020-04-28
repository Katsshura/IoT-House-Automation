using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Infra.Database.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User {Email = "something1@test.com", Username = "test1"},
            new User {Email = "something2@test.com", Username = "test2"}
        };

        public bool IsCredentialsValid(Login login)
        {
            return _users.Any(u => u.Username.Equals(login.Username));
        }

        public DataSet GetUserInformation(string username)
        {
            var ds = new DataSet();
            var dt = new DataTable();

            dt.Columns.Add("Email");
            dt.Columns.Add("Username");

            var result = _users.FirstOrDefault(u => u.Username.Equals(username));

            if (result != null)
            {
                dt.Rows.Add(result.Email, result.Username);
            }

            ds.Tables.Add(dt);
            return ds;
        }
    }
}
