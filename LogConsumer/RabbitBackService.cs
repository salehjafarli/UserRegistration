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
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
