using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using IoT.House.Automation.Libraries.Mapper.Attributes;
using IoT.House.Automation.Libraries.UnitTests.Mapper.Enums;

namespace IoT.House.Automation.Libraries.UnitTests.Mapper.Models
{
    public class EventModelA
    {
        public long DummyLong { get; set; }
        public string DummyString { get; set; }
        public TestingEnumA DummyTestingEnumA { get; set; }
        public virtual Type Type => this.GetType();
    }

    public class EventModelB : EventModelA
    {
        public object DummyParam1 { get; set; }
        public object DummyParam2 { get; set; }
        public override Type Type => this.GetType();
    }

    public class EventModelC
    {
        public string DummyString { get; set; }
        public string Type { get; set; }
        public Dictionary<string, dynamic> DummyParameters { get; set; }
    }

    public class EventModelD
    {
        public string DummyString { get; set; }
        public string Type { get; set; }

        [Mapper("DummyParameters", typeof(EventModelD), false, true)]
        public Dictionary<string, dynamic> DummyParameters { get; set; }
    }

    public class EventModelE
    {
        public string DummyString { get; set; }
        public string Type { get; set; }

        [Mapper("DummyParameters", typeof(EventModelE),false, false)]
        public Dictionary<string, dynamic> DummyParameters { get; set; }
    }
}
