using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IoT.House.Automation.Libraries.Database.SqlServer.Config;

namespace IoT.House.Automation.Libraries.Database.SqlServer.DataAccess
{
    public sealed class SqlServerDataAccess
    {
        private readonly ISqlServerConfiguration _serverConfiguration;

        public SqlServerDataAccess(ISqlServerConfiguration serverConfiguration)
        {
            _serverConfiguration = serverConfiguration;
        }

        public void ExecuteFromProcedure(string schema, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            using (var cmd = new SqlCommand($"[{schema}].[{procedureName}]", _serverConfiguration.Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var sqlParameter in parameters)
                {
                    cmd.Parameters.Add(sqlParameter);
                }

                try
                {
                    _serverConfiguration.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    _serverConfiguration.Connection.Close();
                }
            }
        }


        public DataSet ReadFromProcedure(string schema, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            using (var cmd = new SqlCommand($"[{schema}].[{procedureName}]", _serverConfiguration.Connection))
            using (var dataAdapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var sqlParameter in parameters)
                {
                    cmd.Parameters.Add(sqlParameter);
                }

                try
                {
                    _serverConfiguration.Connection.Open();
                    
                    var ds = new DataSet();

                    dataAdapter.Fill(ds);

                    return ds;
                }
                finally
                {
                    _serverConfiguration.Connection.Close();
                }
            }
        }
    }
}
