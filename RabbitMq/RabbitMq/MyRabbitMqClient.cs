using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMq.RabbitMq
{
    public class QueueOptions
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; } = true;
        public bool Exclusive { get; set; } = true;
    }
    
    public abstract class MyRabbitMqClient : IDisposable
    {
        public IOptions<RabbitMqOptions> Config { get;}

        public abstract QueueOptions QueueOptions { get; }
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
            DeclareQueue(QueueOptions);
            
        }
        private QueueDeclareOk DeclareQueue(QueueOptions options)
        {
            return Channel.QueueDeclare(options.Queue,options.Durable,options.AutoDelete,options.Exclusive);
            
        }


        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }



        
    }
}
