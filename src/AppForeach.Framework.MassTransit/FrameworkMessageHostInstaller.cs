using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMessageHostInstaller<THostDefinition>
        where THostDefinition : MessageHostDefinition 
    {
        protected readonly THostDefinition hostDefinition;

        public FrameworkMessageHostInstaller(THostDefinition hostDefinition)
        {
            this.hostDefinition = hostDefinition;
        }

        public virtual void Setup(IBusRegistrationConfigurator bus)
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
