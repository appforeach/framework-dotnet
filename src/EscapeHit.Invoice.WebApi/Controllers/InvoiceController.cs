using System.Threading.Tasks;
using EscapeHit.Invoice.Commands.CreateInvoice;
using EscapeHit.Invoice.Queries.GetInvoiceById;
using EscapeHit.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.Invoice.WebApi.Controllers
{
    public class InvoiceController : WebApiController
    {
        private readonly IOperationMediator operationMediator;

        public InvoiceController(IOperationMediator operationMediator)
        {
            this.operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ObjectResult> GetById(int id)
        {
            return await operationMediator.Execute(new GetInvoiceByIdQuery { Id = id }).OkOrNotFound();
        }

        [HttpPost]
        public async Task<ObjectResult> Create(CreateInvoiceCommand input)
        {
            return await operationMediator.Execute(input).Ok();
        }
    }
}
