namespace IoT.House.Automation.Libraries.ConfigLoader.Abstractions
{
    public interface IConfigLoaderRepository
    {
        object GetConfigValue(string key);
    }
}
