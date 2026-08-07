using MassTransit;

namespace AppForeach.Framework.MassTransit.RabbitMq;

public class RabbitMqFrameworkMessageHost : FrameworkMessageHost<IRabbitMqBusFactoryConfigurator, IRabbitMqReceiveEndpointConfigurator>
{
}
