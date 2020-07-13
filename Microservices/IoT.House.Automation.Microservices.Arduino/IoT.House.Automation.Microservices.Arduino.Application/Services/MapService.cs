using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;

namespace IoT.House.Automation.Microservices.Arduino.Application.Services
{
    public class MapService : IMap
    {
        private readonly IMapper _mapper;

        public MapService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<T> Map<T>(DataTable table) where T : new()
        {
            var mapped = table
                                .AsEnumerable()
                                .Select(row => _mapper.Map<T>(row))
                                .ToList();
            return mapped;
        }
    }
}
