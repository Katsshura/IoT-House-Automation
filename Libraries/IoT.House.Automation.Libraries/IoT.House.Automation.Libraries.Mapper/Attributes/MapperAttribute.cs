using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.House.Automation.Libraries.Mapper.Attributes
{
    public class MapperAttribute: Attribute
    {
        public string CustomFieldName { get; }
        public Type ConvertTo { get; }
        public bool IgnoreOnMapping { get; }
        public bool IsParameter { get; }

        public MapperAttribute() { }

        public MapperAttribute(string fieldName)
        {
            CustomFieldName = fieldName.Trim();
        }

        public MapperAttribute(bool ignoreOnMapping)
        {
            IgnoreOnMapping = ignoreOnMapping;
        }

        public MapperAttribute(string fieldName, bool ignoreOnMapping) : this(fieldName)
        {
            IgnoreOnMapping = ignoreOnMapping;
        }

        public MapperAttribute(string fieldName, Type convertTo, bool ignoreOnMapping) : this(fieldName, ignoreOnMapping)
        {
            ConvertTo = convertTo;
        }

        public MapperAttribute(string fieldName, Type convertTo, bool ignoreOnMapping, bool isParameter) : this(fieldName, convertTo, ignoreOnMapping)
        {
            IsParameter = isParameter;
        }
    }
}
