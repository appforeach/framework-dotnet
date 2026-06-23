using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class RabbitMqFrameworkMessageHost : FrameworkMessageHost<IRabbitMqBusFactoryConfigurator, IRabbitMqReceiveEndpointConfigurator>
    {
    }
}
