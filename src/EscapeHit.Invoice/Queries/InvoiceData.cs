using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeHit.Invoice.Queries
{
    public class InvoiceData
    {
        public int Id { get; set; }

        public string CustomerNumber { get; set; }

        public decimal Amount { get; set; }

        public string Number { get; set; }
    }
}
