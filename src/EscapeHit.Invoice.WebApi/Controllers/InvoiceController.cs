using System.Threading.Tasks;
using AppForeach.Framework;
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
        public async Task<ActionResult> GetById(int id)
        {
            return await operationMediator.Execute(new GetInvoiceByIdQuery { Id = id }).OkOrNotFound();
        }

        public async Task<ActionResult> Create(CreateInvoiceCommand command)
        {
            return await operationMediator.Execute(command).Ok();
        }
    }
}
