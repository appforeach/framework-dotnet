using System.Threading.Tasks;

namespace EscapeHit.Invoice.Queries.GetInvoiceById
{
    public class GetInvoiceByIdHandler
    {
        public Task<GetInvoiceByIdResult> Execute(GetInvoiceByIdQuery query)
        {
            return Task.FromResult(new GetInvoiceByIdResult());
        }
    }
}
