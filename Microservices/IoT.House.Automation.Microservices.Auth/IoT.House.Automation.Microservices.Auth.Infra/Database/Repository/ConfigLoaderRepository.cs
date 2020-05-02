using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;
using IoT.House.Automation.Libraries.Database.SqlServer.Config;
using IoT.House.Automation.Libraries.Database.SqlServer.DataAccess;

namespace IoT.House.Automation.Microservices.Auth.Infra.Database.Repository
{
    public class ConfigLoaderRepository : IConfigLoaderRepository
    {
        private readonly SqlServerDataAccess _dataAccess;

        public ConfigLoaderRepository(ISqlServerConfiguration sqlConfig)
        {
            _dataAccess = new SqlServerDataAccess(sqlConfig);
        }

        public object GetConfigValue(string key)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("KEY", key)
            };

            var result = _dataAccess.ReadFromProcedure("Auth", "sp_getconfig", parameters);

            var rs = result.Tables[0].AsEnumerable().Select(row => row.ItemArray[0]).FirstOrDefault();

            return rs;
        }
    }
}
