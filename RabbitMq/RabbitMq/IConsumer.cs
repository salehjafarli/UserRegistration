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
        void OnEventReceived(object sender,BasicDeliverEventArgs e);
        void Consume(string queue, bool autoAck);
    }
}
