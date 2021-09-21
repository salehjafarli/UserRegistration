using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMq.Events;
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
        public IConsumer<ConsoleLogEvent> Consumer { get; set; }
        public RabbitBackService(IConsumer<ConsoleLogEvent> Consumer)
        {
            this.Consumer = Consumer;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ((ConsumerBase<ConsoleLogEvent>)Consumer).DeclareQueue("Logs", false, false, false);
            while (!stoppingToken.IsCancellationRequested)
            {
                Consumer.Consume("Logs",false);
            }

            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            
            base.Dispose();
        }
    }
}
