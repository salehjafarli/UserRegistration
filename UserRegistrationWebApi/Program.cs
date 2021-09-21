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
            MyRabbitMqClient connection = new MyRabbitMqClient(Options.Create(opts));
            connection.DeclareQueue("Logs", false, false, false);
             Log.Logger = new LoggerConfiguration()
             .WriteTo.RabbitMqQueue(connection.Channel,new MyTextFormatter())
             .CreateLogger();     // add String formatter!
            
            
            
            //ConnectionFactory fac = new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    Port = 5672
            //};
            //var con = fac.CreateConnection();
            //var ch = con.CreateModel();

            //ch.QueueDeclare(queue: "hello",durable: false, exclusive: false,autoDelete: false,arguments: null);


            //string message = "Hello World!";
            //var body = Encoding.UTF8.GetBytes(message);

            //ch.BasicPublish(exchange: "",
            //                     routingKey: "hello",
            //                     basicProperties: null,
            //                     body: body);

            
           

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);

            }
            finally
            {
                Log.CloseAndFlush();
                connection.Dispose();
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
