
using AppForeach.Framework.Automapper;
using AppForeach.Framework.DataType;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceInputMapping
    {
        InvoiceEntity MapFrom(CreateInvoiceCommand input);
    }

    public class CreateInvoiceCommandMapping : BaseMapping<InvoiceEntity, CreateInvoiceCommand>, ICreateInvoiceInputMapping
    {
        private readonly IMapper mapper;
        public CreateInvoiceCommandMapping(IMapper mapper) =>
            this.mapper = mapper;

        public override InvoiceEntity MapFrom(CreateInvoiceCommand input) =>
            mapper.Map<InvoiceEntity>(input);
    }
}
