﻿using RabbitMQ.Client;
using Serilog;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationWebApi.Helpers;

namespace UserRegistrationWebApi.ExtensionFunctions
{
    public static class RabbitMqQueueSinkExtension
    {
        public static LoggerConfiguration RabbitMqQueue(this LoggerSinkConfiguration loggerConfiguration,IModel Channel)
        {
            return loggerConfiguration.Sink(new RabbitMqQueue(Channel));
        }
    } 
}