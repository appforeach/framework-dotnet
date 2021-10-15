using Automatonymous;
using Automatonymous.Binders;

namespace HistoricalMassTransitSagas
{
    public interface ISagaCommandSender
    {
        EventActivityBinder<TInstance, TData> ProduceCommand<TInstance, TData, TMessage>(EventActivityBinder<TInstance, TData> sagaSource, 
            EventMessageFactory<TInstance, TData, TMessage> messageFactory)
            where TInstance : class, SagaStateMachineInstance
            where TData : class
            where TMessage : class;
    }
}
