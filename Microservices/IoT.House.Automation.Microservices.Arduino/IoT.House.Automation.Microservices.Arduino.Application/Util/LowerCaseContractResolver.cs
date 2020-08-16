using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace IoT.House.Automation.Microservices.Arduino.Application.Util
{
    public class LowerCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || char.IsLower(propertyName, 0))
                return propertyName;

            return char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}
