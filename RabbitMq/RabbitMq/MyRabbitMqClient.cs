using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public abstract class MyRabbitMqClient : IDisposable
    {
        public IOptions<RabbitMqOptions> Config { get;}
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }
        public MyRabbitMqClient(IOptions<RabbitMqOptions> Config)
        {
            this.Config = Config;
            CreateConnection(Config.Value);
        }
        private void CreateConnection(RabbitMqOptions opts)
        {
            var fac = new ConnectionFactory()
            {
                HostName = opts.Hostname,
                Port = opts.Port,
                UserName = opts.Username,
                Password = opts.Password

            };
            Connection = fac.CreateConnection();
            Channel= Connection.CreateModel();

            
        }
        public QueueDeclareOk DeclareQueue(string queue,
                                           bool durable,
                                           bool autoDelete,
                                           bool exclusive)
        {
            return Channel.QueueDeclare(queue,durable,autoDelete,exclusive);
            
        }


        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }



        
    }
}
