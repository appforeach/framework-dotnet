using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public interface IServiceBusFrameworkEndpointConfiguration : IFrameworkEndpointConfiguration
{
    void Configure(Action<IServiceBusReceiveEndpointConfigurator> endpointAction);

    void Configure(Action<IBusRegistrationContext, IServiceBusReceiveEndpointConfigurator> endpointAction);
}
