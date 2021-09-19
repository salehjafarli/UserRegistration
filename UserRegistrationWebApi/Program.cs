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

namespace UserRegistrationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ConnectionFactory fac = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            var con = fac.CreateConnection();
            var ch = con.CreateModel();

            ch.QueueDeclare(queue: "hello",durable: false, exclusive: false,autoDelete: false,arguments: null);


            //string message = "Hello World!";
            //var body = Encoding.UTF8.GetBytes(message);

            //ch.BasicPublish(exchange: "",
            //                     routingKey: "hello",
            //                     basicProperties: null,
            //                     body: body);

            
            Log.Logger = new LoggerConfiguration().WriteTo.RabbitMqQueue(ch).CreateLogger(); // add String formatter!

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
                ch.Dispose();
                con.Dispose();
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
