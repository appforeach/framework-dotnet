using AutoMapper;

namespace EscapeHit.Invoice.Queries.GetInvoiceById
{
    internal class GetInvoiceByIdMappingProfile : Profile
    {
        public GetInvoiceByIdMappingProfile()
        {
            CreateMap<InvoiceEntity, GetInvoiceByIdResult>();
        }
    }
}
