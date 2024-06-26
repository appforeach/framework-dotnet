using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public interface IConsumerInstaller
    {
        void AddConsumer(IRegistrationConfigurator registrationConfigurator);

        void ConfigureConsumer(IReceiveEndpointConfigurator receiveEndpointConfigurator, IRegistrationContext registration);
    }
}
