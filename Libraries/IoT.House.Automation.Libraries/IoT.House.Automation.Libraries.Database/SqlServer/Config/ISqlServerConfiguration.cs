using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace IoT.House.Automation.Libraries.Database.SqlServer.Config
{
    public interface ISqlServerConfiguration
    {
        SqlConnection Connection { get; }
    }
}
