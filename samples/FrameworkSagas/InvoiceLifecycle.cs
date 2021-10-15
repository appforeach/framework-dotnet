using FrameworkSagas.Infrastructure;
using FrameworkSagas.Operations;

namespace FrameworkSagas
{
    public class InvoiceLifecycle : BaseLifecycle<InvoiceEntity, InvoiceEntityStatus>
    {
        protected override InvoiceEntityStatus StateBasedOn(InvoiceEntity entity) => entity.Status;

        public InvoiceLifecycle()
        {
            Initial(
                Allow<RegisterInvoiceInput>()
            );

            When(InvoiceEntityStatus.Active,
                Allow<CloseInvoiceInput>()
            );
        }
    }
}
