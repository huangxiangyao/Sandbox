﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.TodoApi.Models;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Myvas.AspNetCore.TodoApi
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
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddLogging();

            // Add framework services.
            services.AddEntityFrameworkSqlite()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connectionString);
                });

            // Add framework services.
            services.AddMvc();

            // Add repositories
            services.AddSingleton<ITodoRepository, TodoRepository>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied.
            services.AddSwaggerGen();

            // Add the detail information for the API.
            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
                {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.Swagger.Model.Contact
                    {
                        Name = "FrankH",
                        Email = "4848285@qq.com",
                        Url = "http://twitter.com/frankh"
                    },
                    License = new Swashbuckle.Swagger.Model.License
                    {
                        Name = "Apache License 2.0",
                        Url = "https://www.apache.org/licenses/LICENSE-2.0.html"
                    }
                });

                // Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                // Set the comments path for the swagger json and ui.
                Directory.GetFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly)
                    .Select(x => Path.GetFullPath(x))
                    .ToList()
                    .ForEach(x => options.IncludeXmlComments(x));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();

            app.SeedApplicationDb();
        }
    }
}
