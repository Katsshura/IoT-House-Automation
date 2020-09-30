using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;
using IoT.House.Automation.Libraries.Database.SqlServer.Config;
using IoT.House.Automation.Libraries.Mapper;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Application.Converters;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Application.Job;
using IoT.House.Automation.Microservices.Arduino.Application.MessageBroker.Events;
using IoT.House.Automation.Microservices.Arduino.Application.Services;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Infra.DeviceEndpoint;
using IoT.House.Automation.Microservices.Arduino.Infra.Mongo;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Config;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Connection;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.EventBus;
using IoT.House.Automation.Microservices.Arduino.Infra.SqlServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;

namespace IoT.House.Automation.Microservices.Arduino.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDomainServices(services);
            ConfigureExternalServices(services);
            ConfigureApplicationServices(services);
            ConfigureMongoDbServices(services);
            ConfigureMessageBroker(services);
            ConfigureSqlServerServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            lifetime.ApplicationStarted.Register(() => OnApplicationStarted(app.ApplicationServices));
        }

        private void OnApplicationStarted(IServiceProvider provider)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;

            scheduler.JobFactory = provider.GetService<HeartbeatJobFactory>();

            var jobDetail = JobBuilder.Create<HeartbeatJob>()
                .WithIdentity("HeartBeatJob")
                .Build();

            var trigger = TriggerBuilder.Create()
                .ForJob(jobDetail)
                .WithCronSchedule(Configuration.GetSection("Cron").Value)
                .WithIdentity("HeartbeatTrigger")
                .StartNow()
                .Build();

            scheduler.ScheduleJob(jobDetail, trigger);
            scheduler.Start();
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<IHeartbeat, HeartbeatService>();
            services.AddSingleton<HeartbeatJobFactory>();
            services.AddSingleton<HeartbeatJob>();
            services.AddSingleton(provider => new HttpClient());
        }

        private void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddSingleton<IMap, MapService>();
            services.AddSingleton<IArduino, ArduinoRepository>();
        }

        private void ConfigureExternalServices(IServiceCollection services)
        {
            services.AddSingleton<IMapper, MapperService>();
            services.AddSingleton<IEventTrigger, ArduinoDeviceEndpoint>();
        }

        private void ConfigureMongoDbServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(p =>
            {
                var client = new MongoClient(Configuration.GetConnectionString("MongoAccess"));
                var pack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
                ConventionRegistry.Register("My Solution Conventions", pack, t => true);
                return client;
            });

            services.AddSingleton<MongoConfig>();
        }

        private void ConfigureMessageBroker(IServiceCollection services)
        {
            services.AddSingleton<RabbitMQConfig>();
            services.AddSingleton<PersisterConnection>();
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
        }

        private void ConfigureSqlServerServices(IServiceCollection services)
        {
            services.AddSingleton<ISqlServerConfiguration>(p =>
                new SqlServerConfiguration(Configuration.GetConnectionString("ArduinoSqlDatabase")));
            services.AddSingleton<IConfigLoaderRepository, ConfigLoaderRepository>();
        }
    }
}
