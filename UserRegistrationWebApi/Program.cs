using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ;
using Serilog.Formatting.Json;
using System.Text;
using UserRegistrationWebApi.ExtensionFunctions;
using Microsoft.Extensions.Options;
using UserRegistrationWebApi.Helpers;
using RabbitMq.RabbitMq;

namespace UserRegistrationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

            RabbitMqOptions opts = new RabbitMqOptions();

            config.GetSection(RabbitMqOptions.RabbitMq).Bind(opts);

            LogPublisher logPublisher = new LogPublisher(Options.Create(opts));

           //  logPublisher.DeclareQueue("Logs", false, false, false);
            Log.Logger = new LoggerConfiguration()
            .WriteTo.RabbitMqQueue(logPublisher,new MyTextFormatter())
            .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                logPublisher.Dispose();
            }
            finally
            {
                Log.CloseAndFlush();
                
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
