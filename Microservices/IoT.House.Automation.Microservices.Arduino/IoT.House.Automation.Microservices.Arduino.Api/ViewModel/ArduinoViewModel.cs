using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.House.Automation.Microservices.Arduino.Api.ViewModel
{
    public class ArduinoViewModel
    {
        [Required]
        [RegularExpression("^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$")]
        public string UniqueIdentifier { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public IEnumerable<EventViewModel> Events { get; set; }
    }
}
