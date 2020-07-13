using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;

namespace IoT.House.Automation.Microservices.Arduino.Infra.Mongo
{
    public class MongoConfig : BaseConfigLoader
    {
        public string Database { get; set; }
        public string Collection { get; set; }

        protected override string ProjectName => GetProjectName();
        protected override string ClassName => GetType().Name;

        public MongoConfig(IConfigLoaderRepository loaderRepository) : base(loaderRepository)
        {
            TriggerLoadingConfig(this);
        }

        private string GetProjectName() => "IoT.House.Automation";
    }
}
