using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IoT.House.Automation.Libraries.ConfigLoader.Service
{
    internal class ConfigLoaderService
    {
        private readonly IConfigLoaderRepository _repository;

        public ConfigLoaderService(IConfigLoaderRepository repository)
        {
            _repository = repository;
        }

        internal void LoadConfig<T>(T instance) where T : BaseConfigLoader
        {

            var properties = typeof(T).GetProperties();
            LoadProperties(instance, properties);

        }

        private void LoadProperties<T>(T instance, IEnumerable<PropertyInfo> properties) where T : BaseConfigLoader
        {
            foreach (var property in properties)
            {
                var repoKey = instance.GetPartialKey() + property.Name;
                var repoValue = _repository.GetConfigValue(repoKey);

                if (repoValue == null) return;

                LoadConfigValueIntoProperty(instance, property, repoValue);
            }
        }

        private static void LoadConfigValueIntoProperty<T>(T instance, PropertyInfo property, object repoValue)
            where T : BaseConfigLoader
        {
            property.SetValue(instance,
                property.PropertyType.IsEnum
                    ? Enum.Parse(property.PropertyType, repoValue.ToString())
                    : Convert.ChangeType(repoValue, property.PropertyType));
        }
    }
}
