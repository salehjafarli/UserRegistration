﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public interface IProducer<T>
    {
        void Publish(T _event);
    }
}
