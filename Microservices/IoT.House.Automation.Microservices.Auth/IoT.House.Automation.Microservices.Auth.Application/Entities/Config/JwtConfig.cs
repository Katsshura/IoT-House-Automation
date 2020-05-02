using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Enums;

namespace IoT.House.Automation.Microservices.Auth.Application.Entities.Config
{
    public class JwtConfig : BaseConfigLoader
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public ExpirationType ExpirationType { get; set; }

        protected override string ProjectName => GetProjectName();
        protected override string ClassName => GetType().Name;

        public JwtConfig(IConfigLoaderRepository loaderRepository) : base(loaderRepository)
        {
            TriggerLoadingConfig(this);
        }

        private string GetProjectName() => "IoT.House.Automation";
    }
}
