using AutoMapper;

namespace EscapeHit.Invoice.Queries.GetInvoiceById
{
    public interface IGetInvoiceByIdResultMapping
    {
        GetInvoiceByIdResult MapFrom(InvoiceEntity entity);
    }

    public class GetInvoiceByIdResultMapping : IGetInvoiceByIdResultMapping
    {
        private readonly IMapper mapper;
        public GetInvoiceByIdResultMapping(IMapper mapper) =>
            this.mapper = mapper;

        public GetInvoiceByIdResult MapFrom(InvoiceEntity entity) =>
            mapper.Map<GetInvoiceByIdResult>(entity);
    }
}
