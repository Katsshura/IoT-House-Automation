using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IoT.House.Automation.Libraries.Database.SqlServer.Config;
using IoT.House.Automation.Libraries.Database.SqlServer.DataAccess;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Models;

namespace IoT.House.Automation.Microservices.Auth.Infra.Database.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SqlServerDataAccess _dataAccess;

        public AuthRepository(ISqlServerConfiguration sqlConfig)
        {
            _dataAccess = new SqlServerDataAccess(sqlConfig);
        }

        public bool IsCredentialsValid(Login login)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("Username", login.Username),
                new SqlParameter("Password", login.Password)
            };

            var result = _dataAccess.ReadFromProcedure("Auth", "sp_iscredentialvalid", parameters);

            return result.Tables[0].Rows.Count > 0;
        }

        public DataSet GetUserInformation(string username)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("Username", username)
            };

            var result = _dataAccess.ReadFromProcedure("Auth", "sp_getuser", parameters);

            return result;
        }
    }
}
