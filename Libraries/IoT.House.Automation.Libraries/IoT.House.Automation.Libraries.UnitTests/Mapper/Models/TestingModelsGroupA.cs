using IoT.House.Automation.Libraries.Mapper.Attributes;
using IoT.House.Automation.Libraries.UnitTests.Mapper.Enums;

namespace IoT.House.Automation.Libraries.UnitTests.Mapper.Models
{
    public class TestingModelA
    {
        public string DummyString { get; set; }
        public int DummyInteger { get; set; }
        public bool DummyBoolean { get; set; }
        public double DummyDouble { get; set; }
        public TestingEnumA DummyEnumA { get; set; }
    }

    public class TestingModelB
    {
        [Mapper("String")]
        public string DummyString { get; set; }

        [Mapper("Integer")]
        public int DummyInteger { get; set; }
        public bool DummyBoolean { get; set; }
        public double DummyDouble { get; set; }

        [Mapper("EnumA")]
        public TestingEnumA DummyEnumA { get; set; }
    }

    public class TestingModelC
    {
        public string DummyString { get; set; }
        public int DummyInteger { get; set; }
        
        [Mapper(true)]
        public bool DummyBoolean { get; set; }
        public double DummyDouble { get; set; }

        [Mapper(true)]
        public TestingEnumA DummyEnumA { get; set; }
    }
}