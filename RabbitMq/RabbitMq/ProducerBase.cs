using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public abstract class ProducerBase<T> : MyRabbitMqClient,IProducer<T>
    {
        public abstract string Exchange { get; }
        public abstract string RoutingKey { get; }
        public ProducerBase(IOptions<RabbitMqOptions> opts) : base(opts)
        {
               
        }

        public void Publish(T _event)
        {
            byte[] body = JsonSerializer.SerializeToUtf8Bytes <T>(_event);            
            Channel.BasicPublish(Exchange,RoutingKey,false,null, body);
        }
    }
}
