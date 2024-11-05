using MassTransit;

namespace AppForeach.Framework.MassTransit;

public class FrameworkMediatorConsumerInstaller<TMessage> : IConsumerInstaller
    where TMessage : class
{
    public ConsumerConfigurationBuilder<FrameworkMediatorConsumer<TMessage>> ConfigurationBuilder { get; set; } = new();


    public void AddConsumer(IRegistrationConfigurator registrationConfigurator)
    {
        ArgumentNullException.ThrowIfNull(registrationConfigurator);

        registrationConfigurator.AddConsumer<FrameworkMediatorConsumer<TMessage>>();
    }

    public void ConfigureConsumer(IReceiveEndpointConfigurator receiveEndpointConfigurator, IRegistrationContext registration)
    {
        ArgumentNullException.ThrowIfNull(receiveEndpointConfigurator);
        ArgumentNullException.ThrowIfNull(registration);

        receiveEndpointConfigurator.ConfigureConsumer<FrameworkMediatorConsumer<TMessage>>(registration, ConfigurationBuilder.ConfigureAll);
    }
}

