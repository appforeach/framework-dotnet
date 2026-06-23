using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator>
    {
        public List<Action<IBusRegistrationConfigurator>> BusActions { get; } = new();

        public List<(string, IConsumerInstaller)> Consumers { get; } = new();

        public List<Action<IBusRegistrationContext, TBusFactoryConfigurator>> TransportBusActions { get; } = new();

        public List<(string, Action<IBusRegistrationContext, TEndpointConfigurator>)> EndpointActions { get; } = new();
    }
}
