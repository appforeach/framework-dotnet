using AutoMapper;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    internal class CreateInvoiceMappingProfile : Profile
    {
        public CreateInvoiceMappingProfile()
        {
            CreateMap<CreateInvoiceCommand, InvoiceEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.Ignore());
            CreateMap<InvoiceEntity, CreateInvoiceResult>()
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Number));
        }
    }
}
