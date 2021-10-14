namespace HistoricalBusinessScope
{
    public class InvoiceManager
    {
        private readonly CreateInvoiceValidator validator;
        private readonly InvoiceMapping mapping;
        private readonly InvoiceRepository invoiceRepository;

        public InvoiceManager(CreateInvoiceValidator validator, InvoiceMapping mapping, InvoiceRepository invoiceRepository)
        {
            this.validator = validator;
            this.mapping = mapping;
            this.invoiceRepository = invoiceRepository;
        }

        public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
        {
            using(var scope = new BusinessScope(Operation.CreateInvoice, request))
            {
                validator.Validate(request);

                Invoice invoice = mapping.Map(request);

                int invoiceId = invoiceRepository.Create(invoice);

                return new CreateInvoiceResponse(invoiceId);
            }
        }
    }
}
