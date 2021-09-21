using Microsoft.Extensions.Options;
using RabbitMq.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public abstract class ConsumerBase<T> : MyRabbitMqClient, IConsumer<T>
    {

        public ConsumerBase(IOptions<RabbitMqOptions> opts) : base(opts)
        {

        }
        private void OnEventReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = Encoding.UTF8.GetString(e.Body.ToArray());
            T _event = (T)JsonSerializer.Deserialize(body,typeof(T));
            Handle(_event);


        }

        public abstract void Handle(T _event);

        public void Consume(string queue, bool autoAck)
        {

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += OnEventReceived;
            Channel.BasicConsume(queue,autoAck,consumer);

        }
    }
}
