using RabbitMq.Events;
using RabbitMq.RabbitMq;
using RabbitMQ.Client;
using Serilog;
using Serilog.Configuration;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationWebApi.Helpers;

namespace UserRegistrationWebApi.ExtensionFunctions
{
    public static class RabbitMqQueueSinkExtension
    {
        public static LoggerConfiguration RabbitMqQueue(this LoggerSinkConfiguration loggerConfiguration,IProducer<ConsoleLogEvent> Producer, ITextFormatter Formatter)
        {
            return loggerConfiguration.Sink(new RabbitMqQueue(Producer,Formatter));
        }
    } 
}
