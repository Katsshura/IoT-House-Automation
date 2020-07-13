using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;

namespace IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Config
{
    public class RabbitMQConfig : BaseConfigLoader
    {
        public string Uri { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }

        protected override string ProjectName => GetProjectName();
        protected override string ClassName => GetType().Name;

        public RabbitMQConfig(IConfigLoaderRepository loaderRepository) : base(loaderRepository)
        {
            TriggerLoadingConfig(this);
        }

        private string GetProjectName() => "IoT.House.Automation";
    }
}
