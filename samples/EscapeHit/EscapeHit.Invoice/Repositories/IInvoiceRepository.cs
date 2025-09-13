using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscapeHit.Invoice.Repositories
{
    public interface IInvoiceRepository
    {
        Task Create(InvoiceEntity invoice);

        Task<InvoiceEntity> FindById(int id);
    }
}
