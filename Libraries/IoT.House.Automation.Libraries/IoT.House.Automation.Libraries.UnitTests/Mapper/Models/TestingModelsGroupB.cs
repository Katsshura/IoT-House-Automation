using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using IoT.House.Automation.Libraries.Mapper.Attributes;

namespace IoT.House.Automation.Libraries.UnitTests.Mapper.Models
{
    public class TestingModelD
    {
        public Guid DummyGuid { get; set; }
        public string DummyString { get; set; }
        public IPAddress DummyIpAddress { get; set; }
        public int DummyInteger { get; set; }
        public IEnumerable<EventModelA> DummyEvents { get; set; }
    }

    public class TestingModelE
    {
        [Mapper("DummyGuid")]
        public Guid DummyGuid { get; set; }
        
        [Mapper("DummyString")]
        public string DummyString { get; set; }
        
        public IPAddress DummyIpAddress { get; set; }
        public int DummyInteger { get; set; }

        [Mapper("DummyEvents")]
        public IEnumerable<EventModelD> DummyEvents { get; set; }
    }

    public class TestingModelF
    {
        [Mapper(true)]
        public Guid DummyGuid { get; set; }

        public string DummyString { get; set; }

        public IPAddress DummyIpAddress { get; set; }

        [Mapper(true)]
        public int DummyInteger { get; set; }

        [Mapper("DummyEvents", true)]
        public IEnumerable<EventModelD> DummyEvents { get; set; }
    }

    public class TestingModelG
    {
        [Mapper(true)]
        public Guid DummyGuid { get; set; }

        public string DummyString { get; set; }

        public IPAddress DummyIpAddress { get; set; }

        [Mapper(true)]
        public int DummyInteger { get; set; }

        [Mapper("DummyEvents", false)]
        public IEnumerable<EventModelE> DummyEvents { get; set; }
    }

    public class TestingModelH
    {
        public Guid DummyGuid { get; set; }
        public string DummyString { get; set; }
        public IPAddress DummyIpAddress { get; set; }
        public int DummyInteger { get; set; }
        public IEnumerable<EventModelC> DummyEvents { get; set; }
    }
}
