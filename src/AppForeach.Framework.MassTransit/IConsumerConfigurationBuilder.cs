using MassTransit;

namespace AppForeach.Framework.MassTransit;

public interface IConsumerConfigurationBuilder<TConsumer>
    where TConsumer : class
{
    IConsumerConfigurationBuilder<TConsumer> Configure(Action<IConsumerConfigurator<TConsumer>> action);
}
