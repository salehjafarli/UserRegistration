using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationWebApi.RabbitMq
{
    public class RabbitMqOptions
    {
        public const string RabbitMq = "RabbitMq";
        public string Hostname { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}
