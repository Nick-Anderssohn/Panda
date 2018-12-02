using System;
using Core.Db;
using Core.Db.Dao;
using Core.Db.Model;
using Core.Global;
using Core.Middleware.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Core {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            SetupLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add DI classes here https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1
            services.AddSingleton<EnvVars>();
            services.AddSingleton<DbHelper>();
            services.AddSingleton<PandaDao>();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<PandaContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            // Read up on middleware and configure it in this function. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc(); // Terminates pipeline
        }

        private void SetupLogger() {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information();
            
            // Try to setup logging to elasticsearch. Fallback to to console.
            try {
                Uri elasticsearchUrl = new Uri(Configuration[EnvVars.ElasticsearchLogUrlKey]);
                loggerConfiguration.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(elasticsearchUrl) {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                    });
            } catch (Exception) {
                loggerConfiguration.WriteTo.Console();
            }

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}