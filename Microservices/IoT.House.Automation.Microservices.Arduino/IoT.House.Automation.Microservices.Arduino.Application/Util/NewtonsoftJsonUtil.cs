using System;
using System.Collections.Generic;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Application.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IoT.House.Automation.Microservices.Arduino.Application.Util
{
    public static class NewtonsoftJsonUtil
    {
        public static JsonSerializerSettings UseCustomNewtonsoftSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new IPEndPointConverter());
            settings.Converters.Add(new StringEnumConverter());
            settings.ContractResolver = new LowerCaseContractResolver();
            return settings;
        }
    }
}
