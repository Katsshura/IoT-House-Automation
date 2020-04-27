using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.House.Automation.Libraries.Mapper;
using IoT.House.Automation.Libraries.Mapper.Abstractions;
using IoT.House.Automation.Microservices.Auth.Application.Entities.Config;
using IoT.House.Automation.Microservices.Auth.Application.Services;
using IoT.House.Automation.Microservices.Auth.Application.Services.Jwt;
using IoT.House.Automation.Microservices.Auth.Domain.Interfaces;
using IoT.House.Automation.Microservices.Auth.Domain.Services;
using IoT.House.Automation.Microservices.Auth.Infra.Database.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IoT.House.Automation.Microservices.Auth.Api
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
            ConfigureApplicationServices(services);
            ConfigureDatabaseServices(services);
            ConfigureExternalServices(services);

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
            services.AddScoped<IAuth, AuthService>();
            services.AddSingleton<IMap, MapService>();
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddScoped<JwtConfig>();
            services.AddScoped<IJwt, JwtService>();
        }

        private void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        private void ConfigureExternalServices(IServiceCollection services)
        {
            services.AddSingleton<IMapper, MapperService>();
        }
    }
}
