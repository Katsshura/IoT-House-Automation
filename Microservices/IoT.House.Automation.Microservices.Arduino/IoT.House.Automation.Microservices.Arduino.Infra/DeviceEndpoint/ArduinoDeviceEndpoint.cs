using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Models;
using Newtonsoft.Json;

namespace IoT.House.Automation.Microservices.Arduino.Infra.DeviceEndpoint
{
    public class ArduinoDeviceEndpoint : IEventTrigger
    {
        private readonly HttpClient _client;

        public ArduinoDeviceEndpoint(HttpClient client)
        {
            _client = client;
        }

        public void Emit(ArduinoInfo to, string eventName, object eventValue)
        {
            var content = GetPostContent(eventName, eventValue);
            var url = GetPostUrl(to);
            _client.PostAsync(url, content);
        }

        private string GetPostUrl(ArduinoInfo to)
        {
            var ip = to.IP.ToString();
            var port = to.Port;
            return $"http://{ip}:{port}";
        }

        private StringContent GetPostContent(string eventName, object eventValue)
        {
            var content = new Dictionary<string, object>
            {
                {"event", eventName},
                {"value", eventValue}
            };

            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
    }
}
