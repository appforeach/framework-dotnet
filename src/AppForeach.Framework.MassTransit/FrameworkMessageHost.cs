using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public abstract class FrameworkMessageHost<TBusFactoryConfigurator, TEndpointConfigurator>
    {
        private string lastEndpoint = string.Empty;

        public MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator> HostDefinition { get; } = new ();

        public bool IsMediatorEnabled { get; set; } = true;

        protected void ConfigureBus(Action<IBusRegistrationConfigurator> busAction)
        {
            HostDefinition.BusActions.Add(busAction);
        }

        protected void ClearBusConfig()
        {
            HostDefinition.BusActions.Clear();
        }

        protected void ConfigureTransportBus(Action<TBusFactoryConfigurator> rabbitBusAction)
        {
            ConfigureTransportBus((context, config) => rabbitBusAction(config));
        }

        protected void ConfigureTransportBus(Action<IBusRegistrationContext, TBusFactoryConfigurator> rabbitBusAction)
        {
            HostDefinition.TransportBusActions.Add(rabbitBusAction);
        }

        protected void ClearTransportBusConfig()
        {
            HostDefinition.TransportBusActions.Clear();
        }

        protected void Endpoint(string endpointName, Action<IFrameworkEndpointConfiguration<TEndpointConfigurator>>? endpointAction = null)
        {
            lastEndpoint = endpointName;

            var endpointConfigurator = new FrameworkEndpointConfiguration<TBusFactoryConfigurator, TEndpointConfigurator>(endpointName, HostDefinition);
            endpointAction?.Invoke(endpointConfigurator);
        }
    }
}
