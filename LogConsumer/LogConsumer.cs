using Microsoft.Extensions.Options;
using RabbitMq.Events;
using RabbitMq.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogConsumer
{
    class LogConsumer : ConsumerBase<ConsoleLogEvent>
    {
        public LogConsumer(IOptions<RabbitMqOptions> opts) :base(opts)
        {

        }

        public override QueueOptions QueueOptions => new QueueOptions()
        {

            Queue = "Logs",
            AutoDelete = false,
            Durable = false,
            Exclusive = false
        };

        public override void Handle(ConsoleLogEvent _event)
        {
            Console.WriteLine(_event.Message);
        }
    }
}
