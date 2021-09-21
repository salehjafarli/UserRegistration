using RabbitMq.Events;
using RabbitMq.RabbitMq;
using RabbitMQ.Client;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationWebApi.Helpers
{
    public class RabbitMqQueueSink : ILogEventSink
    {
        public IProducer<ConsoleLogEvent> Producer { get; set; }
        public ITextFormatter Formatter { get; set; }
        public RabbitMqQueueSink(IProducer<ConsoleLogEvent> Producer, ITextFormatter Formatter)
        {
            this.Producer = Producer;
            this.Formatter = Formatter;
        }
        public void Emit(LogEvent logEvent)
        {
            var writer = new StringWriter(new StringBuilder(256));
            Formatter.Format(logEvent, writer);
            ConsoleLogEvent _event = new ConsoleLogEvent { Message = writer.ToString() };

            Producer.Publish(_event);

        }
    }
}
