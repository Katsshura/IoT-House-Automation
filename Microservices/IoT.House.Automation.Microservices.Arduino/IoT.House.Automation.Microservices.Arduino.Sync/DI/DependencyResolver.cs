using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IoT.House.Automation.Libraries.ConfigLoader.Abstractions;
using IoT.House.Automation.Libraries.Database.SqlServer.Config;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Infra.Mongo;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Config;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Connection;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.EventBus;
using IoT.House.Automation.Microservices.Arduino.Infra.SqlServer.Repositories;
using IoT.House.Automation.Microservices.Arduino.Sync.Handlers;
using IoT.House.Automation.Microservices.Arduino.Sync.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RabbitMQ.Client;
using SimpleInjector;

namespace IoT.House.Automation.Microservices.Arduino.Sync.DI
{
    public class DependencyResolver
    {
        private static readonly IServiceCollection Services;
        private static readonly IConfiguration Configuration;
        public static readonly IServiceProvider Provider;

        static DependencyResolver()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            Services = new ServiceCollection();

            Setup();

            Provider = Services.BuildServiceProvider();
        }

        private static void Setup()
        {
            ConfigureMessageBroker();
            ConfigureSyncService();
            ConfigureMongoDbServices();
            ConfigureSqlServerServices();
        }

        private static void ConfigureSyncService()
        {
            Services.AddSingleton<ISync, SyncService>();
            Services.AddScoped<IArduino, ArduinoRepository>();
        }

        private static void ConfigureMessageBroker()
        {
            Services.AddSingleton<RabbitMQConfig>();
            Services.AddSingleton<PersisterConnection>();
            Services.AddSingleton<IEventBus, RabbitMQEventBus>();

            ConfigureHandlers();
        }

        private static void ConfigureHandlers()
        {
            Services.AddSingleton<ArduinoRemovedEventHandler>();
        }

        private static void ConfigureMongoDbServices()
        {
            Services.AddSingleton<IMongoClient>(p =>
            {
                var client = new MongoClient(Configuration.GetConnectionString("MongoAccess"));
                var pack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
                ConventionRegistry.Register("My Solution Conventions", pack, t => true);
                return client;
            });

            Services.AddSingleton<MongoConfig>();
        }

        private static void ConfigureSqlServerServices()
        {
            Services.AddSingleton<ISqlServerConfiguration>(p =>
                new SqlServerConfiguration(Configuration.GetConnectionString("ArduinoSqlDatabase")));
            Services.AddSingleton<IConfigLoaderRepository, ConfigLoaderRepository>();
        }
    }
}
