namespace IoT.House.Automation.Libraries.ConfigLoader.Abstractions
{
    public interface IConfigLoaderRepository
    {
        string GetConfigValue(string key);
        void OpenConn();
        void CloseConn();
    }
}
