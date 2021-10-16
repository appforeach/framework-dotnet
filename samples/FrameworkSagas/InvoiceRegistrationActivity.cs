using System.Collections.Generic;
using FrameworkSagas.Infrastructure;
using FrameworkSagas.Operations;

namespace FrameworkSagas
{
    public class InvoiceRegistrationActivity : BaseActivity<InvoiceRegistrationActivityState>
    {
        public InvoiceRegistrationActivity()
        {
            Initial()
                .Save<AccountInvoceRegistrationInput>((state, message) =>
                {
                    state.AccountNumber = message.AccountNumber;
                    state.CutoffDate = message.CutoffDate;
                });

            Step("PerformCutoff")
                .Send(state => new PerformCutoffInput
                {
                    AccountNumber = state.AccountNumber,
                    CutoffDate = state.CutoffDate
                })
                .Save<PerformCutoffOutput>((state, message) =>
                {
                    state.InvoiceAmount = message.Amount;
                });

            Step("RegisterInvoice")
                .Send(state => new RegisterInvoiceInput
                {
                    AccountNumber = state.AccountNumber,
                    CutoffDate = state.CutoffDate,
                    InvoiceAmount = state.InvoiceAmount
                })
                .Save<RegisterInvoiceOutput>((state, message) =>
                {
                    state.InvoiceId = message.InvoiceId;
                });

            Step("UpdateLedger")
                .If(state => state.InvoiceAmount > 0)
                .Send(state => new UpdateLedgerInput
                {
                    AccountNumber = state.AccountNumber,
                    Transasction = "InvoiceRegistration",
                    Amount = state.InvoiceAmount
                });

            Step("SendCommunication")
                .Send(state => new SendCommunicationInput
                {
                    Template = "Invoice",
                    Parameters = new Dictionary<string, string>
                    {
                        { "invoiceId", state.InvoiceId.ToString() }
                    }
                });
        }
    }
}
