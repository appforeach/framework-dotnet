using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusFrameworkMessageHostInstaller : FrameworkMessageHostInstaller<IServiceBusBusFactoryConfigurator, IServiceBusReceiveEndpointConfigurator>
{
    public ServiceBusFrameworkMessageHostInstaller(MessageHostDefinition<IServiceBusBusFactoryConfigurator, IServiceBusReceiveEndpointConfigurator> hostDefinition)
        : base(hostDefinition)
    {
    }

    public override void Setup(IBusRegistrationConfigurator bus)
    {
        ArgumentNullException.ThrowIfNull(bus);

        base.Setup(bus);

        bus.UsingAzureServiceBus((context, cfg) =>
        {
            foreach (var rabbitMqAction in hostDefinition.TransportBusActions)
            {
                rabbitMqAction.Invoke(context, cfg);
            }

            SetupEndpoints(context, cfg);

            cfg.ConfigureEndpoints(context);
        });
    }

    private void SetupEndpoints(IBusRegistrationContext context, IServiceBusBusFactoryConfigurator busConfigurator)
    {
        foreach (var endpoint in hostDefinition.Consumers.GroupBy(c => c.Item1))
        {
            string endpointName = endpoint.Key;

            busConfigurator.ReceiveEndpoint(endpointName, endpointConfig =>
            {
                foreach (var endpointAction in hostDefinition.EndpointActions.Where(ea => ea.Item1 == endpointName))
                {
                    endpointAction.Item2(context, endpointConfig);
                }

                foreach (var consumerInstaller in endpoint.Select(e => e.Item2))
                {
                    consumerInstaller.ConfigureConsumer(endpointConfig, context);
                }
            });
        }
    }
}

