using System;
using Automatonymous;
using Automatonymous.Binders;

namespace HistoricalMassTransitSagas
{
    public static class SagaExtensions
    {
        public static EventActivityBinder<TInstance, TData> SendCommand<TInstance, TData, TMessage>(
            this EventActivityBinder<TInstance, TData> source,
            EventMessageFactory<TInstance, TData, TMessage> messageFactory)
            where TInstance : class, SagaStateMachineInstance
            where TData : class
            where TMessage : class
        {
            ISagaInfrastructureProvider infrastructureProvider = (source.StateMachine as IHasSagaInfrastructureProvider)?.InfrastructureProvider;

            if (infrastructureProvider == null)
            {
                throw new InvalidOperationException($"Saga must implement {nameof(IHasSagaInfrastructureProvider)} to send commands");
            }

            // routing

            // logging

            var commandSender = infrastructureProvider.CommandSender;

            return commandSender.ProduceCommand(source, context => PrepareMessage(context, messageFactory));
        }

        public static EventActivityBinder<TInstance, TData> PublishEvent<TInstance, TData, TMessage>(
            this EventActivityBinder<TInstance, TData> source,
            EventMessageFactory<TInstance, TData, TMessage> messageFactory)
            where TInstance : class, SagaStateMachineInstance
            where TData : class
            where TMessage : class
        {
            ISagaInfrastructureProvider infrastructureProvider = (source.StateMachine as IHasSagaInfrastructureProvider)?.InfrastructureProvider;

            if (infrastructureProvider == null)
            {
                throw new InvalidOperationException($"Saga must implement {nameof(IHasSagaInfrastructureProvider)} to send commands");
            }

            // routing

            // logging

            var commandSender = infrastructureProvider.CommandSender;

            return commandSender.ProduceCommand(source, context => PrepareMessage(context, messageFactory));
        }

        private static TMessage PrepareMessage<TInstance, TData, TMessage>(
            ConsumeEventContext<TInstance, TData> context,
            EventMessageFactory<TInstance, TData, TMessage> messageFactory)
            where TInstance : class, SagaStateMachineInstance
            where TData : class
            where TMessage : class
        {
            var message = messageFactory(context);

            //message.CorellationId = context.Instance.CorrelationId;

            // message.MessageId = Guid.NewGuid();

            // message.TraceId = infrastructureProvider.Logging.CurrentTraceId;

            return message;
        }
    }
}
