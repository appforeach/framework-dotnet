using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public interface IRabbitMqFrameworkEndpointConfiguration : IFrameworkEndpointConfiguration
    {
        void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction);

        void Configure(Action<IBusRegistrationContext, IRabbitMqReceiveEndpointConfigurator> endpointAction);
    }
}
