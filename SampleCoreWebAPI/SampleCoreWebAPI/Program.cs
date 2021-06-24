using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => {

                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddConsole();
                    logging.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider

                    //log4Net
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddLog4Net("log4net.config");
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {

                    config.AddIniFile("system.ini", optional: false,
                    reloadOnChange: false);

                    config.AddJsonFile("config.json", optional: false, 
                    reloadOnChange: false);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
