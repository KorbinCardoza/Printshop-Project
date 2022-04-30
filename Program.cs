using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Formatting.Elasticsearch;

namespace PrintShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var assembly = Assembly.GetExecutingAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("application", new
                {
                    name = assembly?.Name,
                    version = assembly?.Version?.ToString(),
                    environment = env ?? "Production"
                }, true)
                // Override microsoft's default noisy logs
                // use serilog request logs instead
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                // log additional exception details
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Http("Redacted For Privacy", textFormatter: new ExceptionAsObjectJsonFormatter(), restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
