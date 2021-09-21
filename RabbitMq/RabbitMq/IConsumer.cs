using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public interface IConsumer<T>
    {
        void Handle(T _event);
        void Consume(string queue, bool autoAck);
    }
}
