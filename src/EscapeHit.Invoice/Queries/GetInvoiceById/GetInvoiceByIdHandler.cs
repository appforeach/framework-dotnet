using EscapeHit.Invoice.Repositories;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EscapeHit.Invoice.Queries.GetInvoiceById
{
    public class GetInvoiceByIdHandler
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IGetInvoiceByIdResultMapping outputMapping;

        public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository, IGetInvoiceByIdResultMapping outputMapping)
        {
            this.invoiceRepository = invoiceRepository;
            this.outputMapping = outputMapping;
        }

        public async Task<GetInvoiceByIdResult> Execute(GetInvoiceByIdQuery query)
        {
            var invoice = await invoiceRepository.FindById(query.Id);
            return outputMapping.MapFrom(invoice);
        }
    }
}
