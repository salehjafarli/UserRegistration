using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using RabbitMq.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LogConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {


            await new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<RabbitBackService>();
            }).RunConsoleAsync();




            












        }
    }
}
