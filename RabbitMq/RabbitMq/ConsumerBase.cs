using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public class ConsumerBase<T> : MyRabbitMqClient, IConsumer<T>
    {
        public ConsumerBase(IOptions<RabbitMqOptions> opts) : base(opts)
        {

        }
        public void OnEventReceived<T>(object sender, BasicDeliverEventArgs e)
        {
            var body = Encoding.UTF8.GetString(e.Body.ToArray());
            T message = (T)JsonSerializer.Deserialize(body,typeof(T));

        }
    }
}
