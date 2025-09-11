using MassTransit;
using System;

namespace AppForeach.Framework.MassTransit;

public class FrameworkConsumerInstaller<TMessage, TConsumer> : IConsumerInstaller
    where TMessage : class
    where TConsumer : class, IConsumer<TMessage>
{
    public ConsumerConfigurationBuilder<TConsumer> ConfigurationBuilder { get; set; } = new();


    public void AddConsumer(IRegistrationConfigurator registrationConfigurator)
    {
        ArgumentNullException.ThrowIfNull(registrationConfigurator);

        registrationConfigurator.AddConsumer<TConsumer>();
    }

    public void ConfigureConsumer(IReceiveEndpointConfigurator receiveEndpointConfigurator, IRegistrationContext registration)
    {
        ArgumentNullException.ThrowIfNull(receiveEndpointConfigurator);
        ArgumentNullException.ThrowIfNull(registration);

        receiveEndpointConfigurator.ConfigureConsumer<TConsumer>(registration, ConfigurationBuilder.ConfigureAll);
    }
}
