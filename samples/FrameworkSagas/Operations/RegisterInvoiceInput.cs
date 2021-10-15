using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSagas.Operations
{
    public class RegisterInvoiceInput
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }
    }
}
