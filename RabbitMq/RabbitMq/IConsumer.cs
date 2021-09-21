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
        void OnEventReceived<T>(object sender,BasicDeliverEventArgs e);
    }
}
