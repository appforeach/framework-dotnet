using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalBusinessScope
{
    public class InvoiceMapping
    {
        public Invoice Map(CreateInvoiceRequest request)
        {
            return new Invoice();
        }
    }
}
