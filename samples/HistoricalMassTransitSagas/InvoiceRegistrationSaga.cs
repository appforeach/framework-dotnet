using System.Collections.Generic;
using Automatonymous;
using HistoricalMassTransitSagas.Messages;

namespace HistoricalMassTransitSagas
{
    class InvoiceRegistrationSaga : BaseSaga<InvoiceRegistrationState>
    {
        public State InvoiceRegistrationInitiated { get; set; }

        public State CutoffPerformed { get; set; }

        public State CutoffFailed { get; set; }

        public State InvoiceRegistered { get; set; }

        public State InvoiceFailed { get; set; }

        public State LedgerUpdated { get; set; }

        public State LedgerFailed { get; set; }

        public State CommunicationFailed { get; set; }

        public Event<AccountInvoceRegistrationEvent> AccountInvoceRegistrationSagaEvent { get; set; }

        public Event<CutoffCreatedEvent> CutoffCreatedSagaEvent { get; set; }

        public Event<CutoffFailedEvent> CutoffFailedSagaEvent { get; set; }

        public Event<InvoiceRegisteredEvent> InvoiceRegisteredSagaEvent { get; set; }

        public Event<InvoiceFailedEvent> InvoiceFailedSagaEvent { get; set; }

        public Event<LedgerUpdatedEvent> LedgerUpdatedSagaEvent { get; set; }

        public Event<LedgerFailedEvent> LedgerFailedSagaEvent { get; set; }

        public Event<CommunicationCompletedEvent> CommunicationCompletedSagaEvent { get; set; }

        public Event<CommunicationFailedEvent> CommunicationFailedSagaEvent { get; set; }

        public InvoiceRegistrationSaga(ISagaInfrastructureProvider infrastructureProvider) : base(infrastructureProvider)
        {
            Initially(

                When(AccountInvoceRegistrationSagaEvent)
                    .Then(ctx =>
                    {
                        LogInitial(ctx);

                        ctx.Instance.AccountNumber = ctx.Data.AccountNumber;
                        ctx.Instance.CutoffDate = ctx.Data.CutoffDate;
                    })
                    .TransitionTo(InvoiceRegistrationInitiated)
                    .SendCommand(ctx => new PerformCutoffCommand
                    {
                        AccountNumber = ctx.Instance.AccountNumber,
                        CutoffDate = ctx.Instance.CutoffDate
                    })
            );

            During(InvoiceRegistrationInitiated, CutoffFailed,

                When(CutoffCreatedSagaEvent)
                    .Then(ctx =>
                    {
                        LogEvent(ctx);

                        ctx.Instance.InvoiceAmount = ctx.Data.Amount;
                    })
                    .TransitionTo(CutoffPerformed)
                    .SendCommand(ctx => new RegisterInvoiceCommand
                    {
                        AccountNumber = ctx.Instance.AccountNumber,
                        CutoffDate = ctx.Instance.CutoffDate,
                        InvoiceAmount = ctx.Instance.InvoiceAmount
                    }),

                ExpectFailed(CutoffFailedSagaEvent, CutoffFailed)
            );

            During(CutoffPerformed, InvoiceFailed,

                When(InvoiceRegisteredSagaEvent)
                    .Then(ctx =>
                    {
                        LogEvent(ctx);

                        ctx.Instance.InvoiceId = ctx.Data.InvoiceId;
                    })
                    .TransitionTo(InvoiceRegistered)
                    .SendCommand(ctx => new UpdateLedgerCommand
                    {
                        AccountNumber = ctx.Instance.AccountNumber,
                        Transasction = "InvoiceRegistration",
                        Amount = ctx.Instance.InvoiceAmount
                    }),

                ExpectFailed(InvoiceFailedSagaEvent, InvoiceFailed)

            );

            During(InvoiceRegistered, LedgerFailed,

                When(LedgerUpdatedSagaEvent)
                    .Then(ctx => LogEvent(ctx))
                    .TransitionTo(LedgerUpdated)
                    .SendCommand(ctx => new SendCommunicationCommand
                    {
                        Template = "Invoice",
                        Parameters = new Dictionary<string, string>
                        {
                            { "invoiceId", ctx.Instance.InvoiceId.ToString() }
                        }
                    }),

                ExpectFailed(LedgerFailedSagaEvent, LedgerFailed)
            );

            During(LedgerUpdated,
                
                When(CommunicationCompletedSagaEvent)
                    .Then(ctx => LogFinal(ctx))
                    .Finalize(),

                ExpectFailed(CommunicationFailedSagaEvent, CommunicationFailed)
            );
        }
    }
}
