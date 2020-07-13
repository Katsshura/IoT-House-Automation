using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Spi;

namespace IoT.House.Automation.Microservices.Arduino.Application.Job
{
    public class HeartbeatJobFactory : IJobFactory
    {
        private readonly IServiceProvider _provider;

        public HeartbeatJobFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _provider.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            //var disposable = job as IDisposable;
            //disposable?.Dispose();
        }
    }
}
