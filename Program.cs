﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace DeveloperPracticalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

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

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
