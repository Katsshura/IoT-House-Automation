using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.House.Automation.Microservices.Arduino.Api.ViewModel
{
    public class EventViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        [RegularExpression("^(?i)(Integer|Decimal|String|Boolean)$")]
        public string ExpectedInputType { get; set; }
        
        [RegularExpression("^(?i)(RangeEvent|Event)$")]
        public string EventType { get; set; }

        public Dictionary<string, dynamic> Parameters { get; set; }
    }
}
