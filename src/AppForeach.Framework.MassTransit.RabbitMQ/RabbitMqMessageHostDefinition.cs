using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class RabbitMqMessageHostDefinition : MessageHostDefinition
    {
        public List<Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>> RabbitBusActions { get; } = new();
        
        public List<(string, Action<IBusRegistrationContext, IRabbitMqReceiveEndpointConfigurator>)> EndpointActions { get; } = new();
    }
}
