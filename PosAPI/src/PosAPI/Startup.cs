﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PosAPI.DTO;
using Serilog;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PosAPI.Utility;

namespace PosAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"))
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddDbContext<posprojectContext>(option =>
            option.UseMySql(Configuration.GetConnectionString("MySqlDbConnStr")));

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()                    
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddResponseCompression();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");
            app.UseResponseCompression();
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddSerilog();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateLifetime = true
                }
            });

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Products, ProductsDTO>()
                               .ForMember(d => d.ProductName, sr => sr.ResolveUsing<ProductNameToUtf8Resolver>()); 
                cfg.CreateMap<Salebook, SalebookDTO>();
                cfg.CreateMap<SalesDTO, Sales>()
                             .ForMember(v =>v.Billnum, m=>m.MapFrom(u=>u.Billnum))
                             .ForMember(d => d.ProductName, sr => sr.ResolveUsing<ProductNameToLatin1Resolver>());
                cfg.CreateMap<ShopDetails, ShopDetailsDTO>();
                cfg.CreateMap<Users, UsersDTO>();
                cfg.CreateMap<Sales, SalesDTO>().ForMember(v => v.Billnum, m => m.MapFrom(u => u.Billnum))
                                .ForMember(d => d.ProductName, sr => sr.ResolveUsing<ProductNameToSalesDTOUtf8Resolver>()); ;

            });
            
            app.UseMvc();
        }
    }
}
