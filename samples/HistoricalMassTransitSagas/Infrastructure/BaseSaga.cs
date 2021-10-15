using Automatonymous;
using Automatonymous.Binders;

namespace HistoricalMassTransitSagas
{
    public class BaseSaga<TState> : MassTransitStateMachine<TState>, IHasSagaInfrastructureProvider
        where TState : class, SagaStateMachineInstance
    {
        private readonly ISagaInfrastructureProvider infrastructureProvider;

        public BaseSaga(ISagaInfrastructureProvider infrastructureProvider)
        {
            this.infrastructureProvider = infrastructureProvider;
        }

        public ISagaInfrastructureProvider InfrastructureProvider => infrastructureProvider;

        protected void LogInitial<TInitMessage>(BehaviorContext<TState, TInitMessage> context, string message = "")
        {
        }

        protected virtual void LogEvent<TMessage>(BehaviorContext<TState, TMessage> context, string message = "")
        {
        }

        protected void LogFinal<TFinalMessage>(BehaviorContext<TState, TFinalMessage> context, string details = "")
        {
        }

        protected EventActivityBinder<TState, TFailureMessage> ExpectFailed<TFailureMessage>(Event<TFailureMessage> @event, State toState)
        {
            return When(@event)
                .Then(context =>
                {
                    // custom logging

                    LogEvent(context, "some error details from message");
                })
                .TransitionTo(toState);
        }
    }
}
