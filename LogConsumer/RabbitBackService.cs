using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMq.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogConsumer
{
    class RabbitBackService : BackgroundService 
    {
        public MyRabbitMqClient cli { get; set; }
        private EventingBasicConsumer Start()
        {
            RabbitMqOptions opts = new RabbitMqOptions();
            cli = new MyRabbitMqClient(Options.Create(opts));
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
            return consumer;
                
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                cli.Channel.BasicConsume("Logs", false, consumer);
            }

            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            cli.Dispose();
            base.Dispose();
        }
    }
}
