using Automatonymous;
using HistoricalMassTransitSagas.Messages;

namespace HistoricalMassTransitSagas
{
    public class InvoiceLifecycleSaga : BaseSaga<InvoiceRegistrationState>
    {
        public State Registering { get; set; }

        public State RegistrationFailed { get; set; }

        public State Active { get; set; }

        public State Closing { get; set; }

        public State ClosingFailed { get; set; }

        public Event<RegisterInvoiceCommand> RegisterInvoiceSagaEvent { get; set; }
        
        public Event<InvoicingInvoiceRegisteredEvent> InvoicingRegisteredSagaEvent { get; set; }

        public Event<InvoicingInvoiceRegistrationFailedEvent> InvoicingRegistrationFailedSagaEvent { get; set; }

        public Event<CloseInvoiceCommand> CloseInvoiceSagaEvent { get; set; }

        public Event<InvoicingInvoiceClosedEvent> InvoicingInvoiceClosedSagaEvent { get; set; }

        public Event<InvoicingInvoiceClosingFailedEvent> InvoicingInvoiceClosingFailedSagaEvent { get; set; }

        public InvoiceLifecycleSaga(ISagaInfrastructureProvider infrastructureProvider) : base(infrastructureProvider)
        {
            Initially(
                When(RegisterInvoiceSagaEvent)
                    .Then(ctx =>
                    {
                        LogInitial(ctx);

                        ctx.Instance.AccountNumber = ctx.Data.AccountNumber;
                        ctx.Instance.CutoffDate = ctx.Data.CutoffDate;
                        ctx.Instance.InvoiceAmount = ctx.Data.InvoiceAmount;
                    })
                    .TransitionTo(Registering)
                    .SendCommand(ctx => new InvoicingRegisterInvoiceCommand
                    {
                        AccountNumber = ctx.Instance.AccountNumber,
                        CutoffDate = ctx.Instance.CutoffDate,
                        InvoiceAmount = ctx.Instance.InvoiceAmount
                    })
            );

            During(Registering, RegistrationFailed,
                When(InvoicingRegisteredSagaEvent)
                    .Then(ctx =>
                    {
                        LogEvent(ctx);
                        ctx.Instance.InvoiceId = ctx.Data.InvoiceId;
                    })
                    .TransitionTo(Active)
                    .PublishEvent(ctx => new InvoiceRegisteredEvent
                    {
                        InvoiceId = ctx.Instance.InvoiceId
                    }),

                ExpectFailed(InvoicingRegistrationFailedSagaEvent, RegistrationFailed)
            );

            During(Active,
                When(CloseInvoiceSagaEvent)
                    .Then(ctx => LogEvent(ctx))
                    .TransitionTo(Closing)
                    .SendCommand(ctx => new InvoicingCloseInvoiceCommand
                    {
                        InvoiceId = ctx.Instance.InvoiceId
                    })
            );

            During(Closing, ClosingFailed,
                When(InvoicingInvoiceClosedSagaEvent)
                    .Then(ctx => LogFinal(ctx))
                    .Finalize(),

                ExpectFailed(InvoicingInvoiceClosingFailedSagaEvent, ClosingFailed)
            ); ;
        }
    }
}
