
using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class RabbitMqEndpointConfiguration : EndpointConfiguration
    {

        public Action<IRabbitMqReceiveEndpointConfigurator>? EndpointAction { get; set; }
    }
}
