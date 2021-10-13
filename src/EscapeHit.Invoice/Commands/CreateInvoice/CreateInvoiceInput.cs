using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceInput
    {
        public string CustomerNumber { get; set; }

        public decimal Amount { get; set; }
    }
}
