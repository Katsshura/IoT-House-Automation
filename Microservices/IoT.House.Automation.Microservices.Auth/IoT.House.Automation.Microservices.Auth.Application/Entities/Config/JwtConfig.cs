using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Enums;

namespace IoT.House.Automation.Microservices.Auth.Application.Entities.Config
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public ExpirationType ExpirationType { get; set; }

        public JwtConfig()
        {
            Secret = "defaultkeywhiledatabaseconfigisnotimplemented'";
            ExpirationTime = 2;
            ExpirationType = ExpirationType.Minutes;
        }
    }
}
