using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Main class of the application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Stating point of the program
        /// </summary>
        /// <param name="args">String Array as command line arguments</param>
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            // Sets up SeriLog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            // Dependency Injection setup to inject services used in the application
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ICmsService, CmsService>();
                })
                .UseSerilog()
                .Build();
            var svc = ActivatorUtilities.CreateInstance<CmsService>(host.Services);
            svc.run();
        }

        /// <summary>
        /// Sets up the Config File: appsettings.json
        /// </summary>
        /// <param name="builder">Builder Object</param>
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
