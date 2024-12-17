using AutoMapper;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceOutputMapping
    {
        CreateInvoiceResult MapFrom(InvoiceEntity entity);
    }

    public class CreateInvoiceResultMapping : ICreateInvoiceOutputMapping
    {
        private readonly IMapper mapper;
        public CreateInvoiceResultMapping(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// This is an example of custom mappping without inheriting from BaseMapping
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CreateInvoiceResult MapFrom(InvoiceEntity entity) =>
            mapper.Map<CreateInvoiceResult>(entity);

    }
}
