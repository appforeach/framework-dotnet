using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public abstract class FrameworkMessageHost<THostDefinition>
        where THostDefinition : MessageHostDefinition, new()
    {
        private string lastEndpoint = string.Empty;

        public THostDefinition HostDefinition { get; } = new THostDefinition();

        public bool IsMediatorEnabled { get; set; } = true;

        protected void ConfigureBus(Action<IBusRegistrationConfigurator> busAction)
        {
            HostDefinition.BusActions.Add(busAction);
        }

        protected void ClearBusConfig()
        {
            HostDefinition.BusActions.Clear();
        }
    }
}
