using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace IoT.House.Automation.Libraries.Database.SqlServer.Config
{
    public class SqlServerConfiguration : ISqlServerConfiguration
    {
        public SqlConnection Connection => _connection ?? CreateConn();

        private static SqlConnection _connection;
        private readonly string _conn;

        public SqlServerConfiguration(string conn)
        {
            _conn = conn;
        }

        public SqlServerConfiguration(ConnectionStringSettings connectionString)
        {
            _conn = connectionString.ConnectionString;
        }

        private SqlConnection CreateConn() => _connection = new SqlConnection(_conn);
    }
}
