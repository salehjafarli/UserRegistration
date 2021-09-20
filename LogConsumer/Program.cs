using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using RabbitMq.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LogConsumer
{
    class Program
    {
        static  void Main(string[] args)
        {
            RabbitMqOptions opts = new RabbitMqOptions();
            // config.GetSection(RabbitMqOptions.RabbitMq).Bind(opts);
            using (MyRabbitMqClient cli = new MyRabbitMqClient(Options.Create(opts)))
            {
                var queuestat = cli.DeclareQueue("Logs", false, false, false);
                Console.WriteLine("Start");
                var consumer = new EventingBasicConsumer(cli.Channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                    cli.Channel.BasicAck(ea.DeliveryTag, false);
                };
                cli.Channel.BasicConsume("Logs", false, consumer);
                Console.ReadLine();
            }

            //await new HostBuilder()
            //.ConfigureServices((hostContext, services) =>
            //{
            //    services.AddHostedService<RabbitBackService>();
            //})
            //.RunConsoleAsync();
            // var config = new ConfigurationBuilder()
            //                    .AddJsonFile("appsettings.json",false)
            //                  .Build();

           
            








            
        }
    }
}
