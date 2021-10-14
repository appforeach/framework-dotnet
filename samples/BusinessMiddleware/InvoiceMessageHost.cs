using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessMiddleware
{
    public class InvoiceMessageHost : MessageHost
    {
        public InvoiceMessageHost()
        {
            Consume<CreateInvoiceInput>().OperationName("SomeSpecificInvoiceOperation");
        }
    }
}
