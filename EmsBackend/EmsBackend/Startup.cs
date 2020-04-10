using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EmsBusinessLayer.Interface;
using EmsBusinessLayer.Services;
using EmsRepositoryLayer.Interface;
using EmsRepositoryLayer.Services;

namespace EmsBackend
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ems Api", Version = "v1" });
            });

            services.AddTransient<IStateBusiness, StateBusiness>();
            services.AddTransient<IStateRepository, StateRepository>();

            services.AddTransient<ICityBusiness, CityBusiness>();
            services.AddTransient<ICityRepository, CityRepository>();

            services.AddTransient<IPartyBusiness, PartyBusiness>();
            services.AddTransient<IPartyRepository, PartyRepository>();

            services.AddTransient<IConstituencyBusiness, ConstituencyBusiness>();
            services.AddTransient<IConstituencyRepository, ConstituencyRepository>();

            services.AddTransient<ICandidateBusiness, CandidateBusiness>();
            services.AddTransient<ICandidateRepository, CandidateRepository>();

            services.AddTransient<IElectionBusiness, ElectionBusiness>();
            services.AddTransient<IElectionRepository, ElectionRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ems Api v1");
               c.RoutePrefix = string.Empty;
           });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
