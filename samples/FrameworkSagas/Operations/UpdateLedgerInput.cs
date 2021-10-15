using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSagas.Operations
{
    public class UpdateLedgerInput
    {
        public string AccountNumber { get; set; }

        public string Transasction { get; set; }

        public decimal Amount { get; set; }
    }
}
