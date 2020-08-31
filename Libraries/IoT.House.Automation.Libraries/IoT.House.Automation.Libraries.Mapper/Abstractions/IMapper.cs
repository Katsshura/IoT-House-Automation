using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IoT.House.Automation.Libraries.Mapper.Abstractions
{
    public interface IMapper
    {
        T Map<T>(DataRow source) where T : new();
        TResult Map<TSource, TResult>(TSource source) where TResult : new();
    }
}
