using RabbitMQ.Client;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationWebApi.Helpers
{
    public class RabbitMqQueue : ILogEventSink
    {
        public IModel Channel { get; set; }
        public RabbitMqQueue(IModel Channel)
        {
            this.Channel = Channel;
        }
        public void Emit(LogEvent logEvent)
        {
            var bytes = Encoding.UTF8.GetBytes(logEvent.RenderMessage());
            Channel.BasicPublish(exchange: "",routingKey: "hello", basicProperties: null,body: bytes);
        }
    }
}
