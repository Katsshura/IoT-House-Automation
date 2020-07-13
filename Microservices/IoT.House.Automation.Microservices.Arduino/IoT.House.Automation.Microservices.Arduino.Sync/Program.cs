using System;
using System.Threading;
using IoT.House.Automation.Microservices.Arduino.Sync.DI;
using IoT.House.Automation.Microservices.Arduino.Sync.Service;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.House.Automation.Microservices.Arduino.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            var sync = DependencyResolver.Provider.GetService<ISync>();
            sync.Start();
            new Thread(() => Console.ReadLine()).Start();
        }
    }
}
