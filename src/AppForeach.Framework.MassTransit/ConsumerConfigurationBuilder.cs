﻿using MassTransit;

namespace AppForeach.Framework.MassTransit;

public class ConsumerConfigurationBuilder<TConsumer> : IConsumerConfigurationBuilder<TConsumer>
    where TConsumer : class
{
    private readonly List<Action<IConsumerConfigurator<TConsumer>>> actions = [];

    public IConsumerConfigurationBuilder<TConsumer> Configure(Action<IConsumerConfigurator<TConsumer>> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        actions.Add(action);
        return this;
    }

    public void ConfigureAll(IConsumerConfigurator<TConsumer> cfg)
    {
        ArgumentNullException.ThrowIfNull(cfg);

        foreach (var action in actions) 
        {
            action(cfg);
        }
    }
}
