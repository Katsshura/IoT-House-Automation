using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper.Attributes;

namespace IoT.House.Automation.Microservices.Arduino.Api.ViewModel
{
    public class EventViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression("^(?i)(Integer|Decimal|String|Boolean)$")]
        public string ExpectedInputType { get; set; }

        [Required]
        [RegularExpression("^(?i)(RangeEvent|Event)$")]
        [Mapper(true)]
        public string EventType { get; set; }

        public Dictionary<string, dynamic> Parameters { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is EventViewModel compareObj)) return false;

            return Name.Equals(compareObj.Name);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            hash *= 10 + Name.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
