using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProjectManagement.API.Configuration;
using ProjectManagement.API.Helpers;
using ProjectManagement.EntityFramework.Shared.DbContexts;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectManagement.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                await ApplyDbMigrationsWithDataSeedAsync(args, configuration, host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static async Task ApplyDbMigrationsWithDataSeedAsync(string[] args, IConfiguration configuration,
            IHost host)
        {
            var seedConfiguration = configuration.GetSection(nameof(SeedConfiguration)).Get<SeedConfiguration>();
            var databaseMigrationsConfiguration = configuration.GetSection(nameof(DatabaseMigrationsConfiguration))
                .Get<DatabaseMigrationsConfiguration>();

            await DbMigrationHelpers
                .ApplyDbMigrationsWithDataSeedAsync<ApplicationDbContext>(host, seedConfiguration,
                    databaseMigrationsConfiguration);
        }

        private static IConfiguration GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);

            configurationBuilder.Build();

            configurationBuilder.AddEnvironmentVariables();

            return configurationBuilder.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    var configurationRoot = configApp.Build();

                    configApp.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
                    configApp.AddJsonFile("projectdata.json", optional: true, reloadOnChange: true);
                    configApp.AddJsonFile("userdata.json", optional: true, reloadOnChange: true);

                    var env = hostContext.HostingEnvironment;

                    configApp.AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    configApp.AddJsonFile($"projectdata.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    configApp.AddJsonFile($"userdata.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostContext, loggerConfig) =>
                {
                    loggerConfig
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .Enrich.WithProperty("ApplicationName", hostContext.HostingEnvironment.ApplicationName);
                });
    }
}