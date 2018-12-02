using System;
using System.Threading;
using Core.Db.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateWebHostBuilder(args).Build();

            PerformDbMigrations(host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:8080")
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddEnvironmentVariables("PANDA_"); // Add all environment variables prefixed with "PANDA_"
                })
                .UseKestrel() // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1
                .UseStartup<Startup>();
        }

        private static void PerformDbMigrations(IWebHost host) {
            using (IServiceScope scope = host.Services.CreateScope()) {
                // Try up to 10 times to perform database migrations
                bool keepTrying = true;
                for (int i = 0; i < 10 && keepTrying; i++) {
                    keepTrying = false;
                    try {
                        var ctx = scope.ServiceProvider.GetService<PandaContext>();
                        ctx.Database.Migrate();
                    } catch (Exception e) {
                        Log.Warning("Failed to perform database migrations: {@exception}", e);
                        keepTrying = true;
                        // wait 1 second
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}