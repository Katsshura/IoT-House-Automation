using IoT.House.Automation.Libraries.ConfigLoader.Service;
using System;

namespace IoT.House.Automation.Libraries.ConfigLoader.Abstractions
{
    public abstract class BaseConfigLoader
    {
        protected abstract string ProjectName { get; }
        protected abstract string ClassName { get; }

        private readonly ConfigLoaderService _loaderService;

        protected BaseConfigLoader(IConfigLoaderRepository loaderRepository)
        {
            _loaderService = new ConfigLoaderService(loaderRepository);
        }

        public string GetPartialKey() => $"{ProjectName}.{this.RemoveConfigSuffixFromClassName(ClassName)}.";

        private string RemoveConfigSuffixFromClassName(string className)
        {
            if (!className.ToLower().EndsWith("config"))
                throw new InvalidOperationException("Class name is invalid! It must have Config as suffix on class name");

            var name = className.Remove(className.Length - 6);
            return name;
        }

        protected void TriggerLoadingConfig<T>(T instance) where T : BaseConfigLoader
        {
            _loaderService.LoadConfig(instance);
        }
    }
}
