using System.Threading.Tasks;

namespace IoT.House.Automation.Microservices.Arduino.Application.Interfaces
{
    public interface IHeartbeat
    {
        Task Start();
    }
}
