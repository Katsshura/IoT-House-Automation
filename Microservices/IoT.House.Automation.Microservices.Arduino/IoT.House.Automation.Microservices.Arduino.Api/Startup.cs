using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Arduino.Application.Services;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Infra.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

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
            ConfigureMongoDbServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddSingleton<IMap, MapService>();
            services.AddScoped<IArduino, ArduinoRepository>();
        }

        private void ConfigureExternalServices(IServiceCollection services)
        {
            services.AddSingleton<IMapper, MapperService>();
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
        }
    }
}
