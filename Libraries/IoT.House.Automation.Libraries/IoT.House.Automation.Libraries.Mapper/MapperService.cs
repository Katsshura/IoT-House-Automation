using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Libraries.Mapper.Attributes;

namespace IoT.House.Automation.Libraries.Mapper
{
    public class MapperService : IMapper
    {
        public T Map<T>(DataRow source) where T : new()
        {
            var properties = GetProperties<T>();
            var instance = new T();

            foreach (var property in properties)
            {
                SetPropertyValueFromDataRow(property, instance, source);
            }

            return instance;
        }

        public TResult Map<TSource, TResult>(TSource source) where TResult : new()
        {
            var instance = new TResult();
            var properties = GetProperties<TSource>();

            foreach (var property in properties)
            {
                var attribute = GetMapperAttributeFromProperty(property);
                var qualifiedName = GetQualifiedName(attribute, property);

                var data = property.GetValue(source);

                if (IsAttributeIgnoreOnMapping(attribute) || data == default || IsGenericTypeAnEnumerable(property.PropertyType)) continue;

                SetPropertyValue(typeof(TResult).GetProperty(qualifiedName), instance, data);
            }
         
            return instance;
        }

        private IEnumerable<PropertyInfo> GetProperties<T>()
        {
            if (IsGenericTypeAnEnumerable(typeof(T)))
            {
                throw new NotSupportedException("This Mapper Doesn't Support Collections Types as Result'");
            }

            var properties = typeof(T).GetProperties();
            return properties;
        }

        private void SetPropertyValueFromDataRow(PropertyInfo property, object instance, DataRow source)
        {
            InstantiateComplexObject(property, instance, source);

            var columns = source.Table.Columns;
            var attribute = GetMapperAttributeFromProperty(property);

            var qualifiedName = GetQualifiedName(attribute, property);

            foreach (DataColumn column in columns)
            {
                //if (attribute == null || attribute.IgnoreOnMapping) continue;
                if (!column.Caption.Equals(qualifiedName, StringComparison.InvariantCultureIgnoreCase) || IsAttributeIgnoreOnMapping(attribute)) continue;

                var data = source[qualifiedName];
                SetPropertyValue(property, instance, data);
                break;
            }
        }

        private static bool IsAttributeIgnoreOnMapping(MapperAttribute attribute)
        {
            return attribute != null && attribute.IgnoreOnMapping;
        }

        private void InstantiateComplexObject(PropertyInfo property, object instance, object source)
        {
            if (property.PropertyType.FullName != null && !property.PropertyType.FullName.ToLowerInvariant().Contains("system"))
            {
                InstantiateObjectAndFulfill(property, instance, source);
            }
        }

        private MapperAttribute GetMapperAttributeFromProperty(PropertyInfo property)
        {
            return (MapperAttribute)property.GetCustomAttributes(true)
                .Where(at => at.GetType() == typeof(MapperAttribute))
                .ToArray().FirstOrDefault();
        }

        private void InstantiateObjectAndFulfill(PropertyInfo property, object instance, object source)
        {
            var propertyInstance = property.GetValue(instance);

            if (propertyInstance == null)
            {
                propertyInstance = Activator.CreateInstance((property.PropertyType));
                property.SetValue(instance, propertyInstance);
            }

            var props = propertyInstance.GetType().GetProperties();

            foreach (var prop in props)
            {
                SetPropertyValue(prop, propertyInstance, source);
            }
        }

        private void SetPropertyValue(PropertyInfo property, object instance, object data)
        {
            if (data == null) return;

            if (property.PropertyType.IsEnum)
            {
                property.SetValue(instance, Enum.Parse(property.PropertyType, data.ToString(), true));
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                DateTime.TryParse(data.ToString(), out var date);
                property.SetValue(instance, date);
            }
            else if (property.PropertyType == typeof(IPAddress))
            {
                IPAddress.TryParse(data.ToString(), out var address);
                property.SetValue(instance, address);
            }
            else
            {
                property.SetValue(instance, ConvertType(property.PropertyType, data));
            }
        }

        private object ConvertType(Type convertTo, object value)
        {
            try
            {
                return Convert.ChangeType(value, convertTo);
            }
            catch (InvalidCastException)
            {
                var converter = TypeDescriptor.GetConverter(convertTo);

                return converter.CanConvertFrom(value.GetType())
                    ? converter.ConvertFrom(value)
                    : converter.ConvertFromString(value.ToString());
            }
        }

        private string GetQualifiedName(MapperAttribute attribute, PropertyInfo property)
        {
            return string.IsNullOrWhiteSpace(attribute?.CustomFieldName) ? property.Name : attribute.CustomFieldName;
        }

        private bool IsGenericTypeAnEnumerable(params Type[] types)
        {
            return types.Any(type => type.GetInterface("IEnumerable") != null && type != typeof(string));
        }
    }
}
