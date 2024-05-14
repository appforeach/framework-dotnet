
using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class EndpointConfiguration
    {
        public required string EndpointName { get; set; }

        public Action<IRabbitMqReceiveEndpointConfigurator>? EndpointAction { get; set; }
    }
}
