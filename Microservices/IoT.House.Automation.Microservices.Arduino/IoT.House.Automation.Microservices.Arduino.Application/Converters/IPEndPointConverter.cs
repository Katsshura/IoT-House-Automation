using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoT.House.Automation.Microservices.Arduino.Application.Converters
{
    public class IPEndPointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPEndPoint));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ep = (IPEndPoint)value;
            var jo = new JObject {{"Address", JToken.FromObject(ep.Address, serializer)}, {"Port", ep.Port}};
            jo.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var address = jo["Address"].ToObject<IPAddress>(serializer);
            var port = (int)jo["Port"];
            return new IPEndPoint(address, port);
        }
    }
}
