using AccountAndTransactions.genericobj;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountAndTransactions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
         .Build();

            var logPath = new AppSettings();
            config.GetSection("AppSettingConfig").Bind(logPath);

            Log.Logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .MinimumLevel.Debug()
                   .WriteTo.File(
                      $@"{logPath.LogPath}\MobileMoneyAdmin_{DateTime.Now.ToString("ddMMyyyy")}.txt",
                  fileSizeLimitBytes: 10_000_000,
                  rollOnFileSizeLimit: true,
                  shared: true,
                  flushToDiskInterval: TimeSpan.FromSeconds(1))
                 .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                // Close and flush the log.
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