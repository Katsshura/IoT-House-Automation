using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using IoT.House.Automation.Libraries.Mapper;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Libraries.UnitTests.Mapper.Enums;
using IoT.House.Automation.Libraries.UnitTests.Mapper.Models;
using Xunit;

namespace IoT.House.Automation.Libraries.UnitTests.Mapper
{
    public class MapperTests
    {
        [Fact]
        public void MapFromDataRow_IsValid_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var dataTable = new DataTable();

            dataTable.Columns.AddRange(new[]
            {
                new DataColumn("DummyString"), new DataColumn("DummyInteger"), new DataColumn("DummyBoolean"),
                new DataColumn("DummyDouble"), new DataColumn("DummyEnumA")
            });

            var source = dataTable.NewRow();

            source.ItemArray = new object[] { "DummyStringValue", 10, true, 2.0, 2 };

            dataTable.Dispose();

            var result = mapper.Map<TestingModelA>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelA>(result);
            Assert.Equal("DummyStringValue", result.DummyString);
            Assert.Equal(10, result.DummyInteger);
            Assert.True(result.DummyBoolean);
            Assert.Equal(2.0, result.DummyDouble);
            Assert.Equal(TestingEnumA.Second, result.DummyEnumA);
        }

        [Fact]
        public void MapFromDataRow_IsValidWithCustomFieldName_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var dataTable = new DataTable();

            dataTable.Columns.AddRange(new[]
            {
                new DataColumn("String"), new DataColumn("Integer"), new DataColumn("DummyBoolean"),
                new DataColumn("DummyDouble"), new DataColumn("EnumA")
            });

            var source = dataTable.NewRow();

            source.ItemArray = new object[] { "StringValue", 4590, true, 1000.0, 1 };

            dataTable.Dispose();

            var result = mapper.Map<TestingModelB>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelB>(result);
            Assert.Equal("StringValue", result.DummyString);
            Assert.Equal(4590, result.DummyInteger);
            Assert.True(result.DummyBoolean);
            Assert.Equal(1000.0, result.DummyDouble);
            Assert.Equal(TestingEnumA.First, result.DummyEnumA);
        }

        [Fact]
        public void MapFromDataRow_IsValidWithIgnore_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var dataTable = new DataTable();

            dataTable.Columns.AddRange(new[]
            {
                new DataColumn("DummyString"), new DataColumn("DummyInteger"), new DataColumn("DummyBoolean"),
                new DataColumn("DummyDouble"), new DataColumn("DummyEnumA")
            });

            var source = dataTable.NewRow();

            source.ItemArray = new object[] { "DummyStringValueV2", 900, true, 2000.0, 4 };

            dataTable.Dispose();

            var result = mapper.Map<TestingModelC>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelC>(result);
            Assert.Null(result.DummyString);
            Assert.Equal(900, result.DummyInteger);
            Assert.True(result.DummyBoolean);
            Assert.Equal(0, result.DummyDouble);
            Assert.Equal(TestingEnumA.Fourth, result.DummyEnumA);
        }

        [Fact]
        public void MapBetweenClasses_IsValidWithNoParameters_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var source = new TestingModelH
            {
                DummyGuid = Guid.NewGuid(),
                DummyInteger = 250,
                DummyString = "DummyString",
                DummyIpAddress = IPAddress.Parse("192.168.0.1"),
                DummyEvents = new[]
                {
                    new EventModelC
                    {
                        DummyString = "DummyEventString",
                        Type = "EventModelA",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString" },
                            { "DummyParam2", 299 }
                        }
                    },
                    new EventModelC
                    {
                        DummyString = "DummyEventString2",
                        Type = "EventModelA",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString2" },
                            { "DummyParam2", 400 }
                        }
                    }
                }
            };

            var result = mapper.Map<TestingModelH, TestingModelD>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelD>(result);
            Assert.Equal(source.DummyString, result.DummyString);
            Assert.Equal(source.DummyInteger, result.DummyInteger);
            Assert.Equal(source.DummyGuid, result.DummyGuid);
            Assert.Equal(source.DummyIpAddress, result.DummyIpAddress);
        }

        [Fact]
        public void MapBetweenClasses_IsValidWithParameters_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var source = new TestingModelE
            {
                DummyGuid = Guid.NewGuid(),
                DummyInteger = 250,
                DummyString = "DummyString",
                DummyIpAddress = IPAddress.Parse("192.168.0.1"),
                DummyEvents = new[]
                {
                    new EventModelD
                    {
                        DummyString = "DummyEventString",
                        Type = "EventModelB",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString" },
                            { "DummyParam2", 299 }
                        }
                    },
                    new EventModelD
                    {
                        DummyString = "DummyEventString2",
                        Type = "EventModelB",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString2" },
                            { "DummyParam2", 400 }
                        }
                    }
                }
            };

            var result = mapper.Map<TestingModelE, TestingModelD>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelD>(result);
            Assert.Equal(source.DummyString, result.DummyString);
            Assert.Equal(source.DummyInteger, result.DummyInteger);
            Assert.Equal(source.DummyGuid, result.DummyGuid);
            Assert.Equal(source.DummyIpAddress, result.DummyIpAddress);
        }

        [Fact]
        public void MapBetweenClasses_IsValidWithIgnore_ReturnsTheObjectFulfilled()
        {
            IMapper mapper = new MapperService();
            var source = new TestingModelF
            {
                DummyGuid = Guid.NewGuid(),
                DummyInteger = 250,
                DummyString = "DummyString",
                DummyIpAddress = IPAddress.Parse("192.168.0.1"),
                DummyEvents = new[]
                {
                    new EventModelD
                    {
                        DummyString = "DummyEventString",
                        Type = "EventModelB",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString" },
                            { "DummyParam2", 299 }
                        }
                    },
                    new EventModelD
                    {
                        DummyString = "DummyEventString2",
                        Type = "EventModelB",
                        DummyParameters = new Dictionary<string, dynamic>
                        {
                            { "DummyParam1", "DummyString2" },
                            { "DummyParam2", 400 }
                        }
                    }
                }
            };

            var result = mapper.Map<TestingModelF, TestingModelD>(source);

            Assert.NotNull(result);
            Assert.IsType<TestingModelD>(result);
            Assert.Equal(source.DummyString, result.DummyString);
            Assert.Equal(0, result.DummyInteger);
            Assert.Equal(Guid.Empty, result.DummyGuid);
            Assert.Equal(source.DummyIpAddress, result.DummyIpAddress);
            Assert.Null(result.DummyEvents);
        }
    }
}