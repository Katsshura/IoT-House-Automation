using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using Quartz;

namespace IoT.House.Automation.Microservices.Arduino.Application.Job
{
    public class HeartbeatJob : IJob
    {
        private readonly IHeartbeat _heartbeat;

        public HeartbeatJob(IHeartbeat heartbeat)
        {
            _heartbeat = heartbeat;
        }


        public Task Execute(IJobExecutionContext context)
        {
            return _heartbeat.Start();
        }
    }
}
