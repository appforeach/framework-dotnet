using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public abstract class FrameworkMessageHostInstaller
    {
        public abstract void Setup(IBusRegistrationConfigurator bus);
    }

    public abstract class FrameworkMessageHostInstaller<TBusFactoryConfigurator, TEndpointConfigurator> : FrameworkMessageHostInstaller
    {
        protected readonly MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator> hostDefinition;

        public FrameworkMessageHostInstaller(MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator> hostDefinition)
        {
            this.hostDefinition = hostDefinition;
        }

        public override void Setup(IBusRegistrationConfigurator bus)
        {
            ArgumentNullException.ThrowIfNull(bus);

            foreach (var busAction in hostDefinition.BusActions)
            {
                busAction(bus);
            }

            foreach (var consumer in hostDefinition.Consumers)
            {
                consumer.Item2.AddConsumer(bus);
            }
        }
    }
}
