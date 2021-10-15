
namespace HistoricalMassTransitSagas
{
    public interface ISagaInfrastructureProvider
    {
        IRoutingService RoutingService { get; }

        ILoggerService LoggerService { get; }

        ISagaCommandSender CommandSender { get; }
    }
}
