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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ems Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
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

            services.AddTransient<IVoteBusiness, VoteBusiness>();
            services.AddTransient<IVoteRepository, VoteRepository>();

            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<IAdminRepository, AdminRepository>();

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

            app.UseCors("AllowOrigin");

            app.UseMvc();
        }
    }
}
