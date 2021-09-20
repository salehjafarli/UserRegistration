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
    public class RabbitMqQueue : ILogEventSink
    {
        public IModel Channel { get; set; }
        public ITextFormatter Formatter { get; set; }
        public RabbitMqQueue(IModel Channel, ITextFormatter Formatter)
        {
            this.Channel = Channel;
            this.Formatter = Formatter;
        }
        public void Emit(LogEvent logEvent)
        {
            var writer = new StringWriter(new StringBuilder(256));
            Formatter.Format(logEvent, writer);
            var bytes = Encoding.UTF8.GetBytes(writer.ToString());
            Channel.BasicPublish(exchange: "",routingKey: "Logs", basicProperties: null,body: bytes);
        }
    }
}
