using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            
            var properties = GetProperties<TResult>();
            var instance = new TResult();

            foreach (var property in properties)
            {
                var row = GenerateDataRowFromObject(source);
                SetPropertyValueFromDataRow(property, instance, row);
            }

            return instance;
        }

        private IEnumerable<PropertyInfo> GetProperties<T>() where T : new()
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
            if (property.PropertyType.FullName != null && !property.PropertyType.FullName.ToLowerInvariant().Contains("system"))
            {
                InstantiateObjectAndFulfill(property, instance, source);
            }

            var columns = source.Table.Columns;
            var attribute = GetMapperAttributeFromProperty(property);

            var qualifiedName = GetQualifiedName(attribute, property);

            foreach (DataColumn column in columns)
            {
                if ((attribute == null || attribute.IgnoreOnMapping) &&
                    !column.Caption.Equals(qualifiedName, StringComparison.InvariantCultureIgnoreCase)) continue;

                var data = source[qualifiedName];
                SetPropertyValue(property, instance, data);
                break;
            }
        }

        private MapperAttribute GetMapperAttributeFromProperty(PropertyInfo property)
        {
            return (MapperAttribute)property.GetCustomAttributes(true)
                .Where(at => at.GetType() == typeof(MapperAttribute))
                .ToArray().FirstOrDefault();
        }

        private void InstantiateObjectAndFulfill(PropertyInfo property, object instance, DataRow source)
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
                property.SetValue(instance, Enum.Parse(property.PropertyType, data.ToString()));
            }
            else
            {
                property.SetValue(instance, Convert.ChangeType(data, property.PropertyType));
            }
        }

        private string GetQualifiedName(MapperAttribute attribute, PropertyInfo property)
        {
            return string.IsNullOrWhiteSpace(attribute?.CustomFieldName) ? property.Name : attribute.CustomFieldName;
        }

        private bool IsGenericTypeAnEnumerable(params Type[] types)
        {
            return types.Any(type => type.GetInterface("IEnumerable") != null);
        }

        private DataRow GenerateDataRowFromObject(object source)
        {
            var properties = source.GetType().GetProperties();
            var dataTable = new DataTable();

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name);
            }

            var dataRow = dataTable.NewRow();

            foreach (var property in properties)
            {
                dataRow[property.Name] = property.GetValue(source).ToString();
            }

            dataTable.Dispose();
            return dataRow;
        }
    }
}
