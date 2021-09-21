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

        public override void Handle(ConsoleLogEvent _event)
        {
            Console.WriteLine(_event.Message);
        }
    }
}
