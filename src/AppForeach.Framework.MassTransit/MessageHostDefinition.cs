using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class MessageHostDefinition
    {
        public List<Action<IBusRegistrationConfigurator>> BusActions { get;  } = new();
        
        public List<(string, IConsumerInstaller)> Consumers { get; } = new();
    }
}
