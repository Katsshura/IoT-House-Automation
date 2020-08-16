using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Application.Converters;
using Newtonsoft.Json;

namespace IoT.House.Automation.Microservices.Arduino.Application.Util
{
    public static class NewtonsoftJsonUtil
    {
        public static JsonSerializerSettings UseCustomNewtonsoftSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new IPEndPointConverter());
            settings.ContractResolver = new LowerCaseContractResolver();
            return settings;
        }
    }
}
