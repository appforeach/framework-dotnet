﻿using System.Threading.Tasks;
using EscapeHit.Invoice.Repositories;
using EscapeHit.Invoice.Services;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceHandler
    {
        private readonly ICreateInvoiceInputMapping inputMapping;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IInvoiceNumberService invoiceNumberService;
        private readonly ICreateInvoiceOutputMapping outputMapping;

        public CreateInvoiceHandler(ICreateInvoiceInputMapping inputMapping,
            IInvoiceRepository invoiceRepository,
            IInvoiceNumberService invoiceNumberService,
            ICreateInvoiceOutputMapping outputMapping)
        {
            this.inputMapping = inputMapping;
            this.invoiceRepository = invoiceRepository;
            this.invoiceNumberService = invoiceNumberService;
            this.outputMapping = outputMapping;
        }

        public async Task<CreateInvoiceResult> Execute(CreateInvoiceCommand input)
        {
            var entity = inputMapping.MapFrom(input);

            entity.Number = invoiceNumberService.GenerateInvoiceNumber(entity);

            await invoiceRepository.Create(entity);

            return outputMapping.MapFrom(entity);
        }
    }
}
