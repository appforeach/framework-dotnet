using System.Threading.Tasks;
using AppForeach.Framework;
using EscapeHit.Invoice.Commands.CreateInvoice;
using EscapeHit.Invoice.Queries.GetInvoiceById;
using EscapeHit.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.Invoice.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IOperationMediator operationMediator;

        public InvoiceController(IOperationMediator operationMediator)
        {
            this.operationMediator = operationMediator;
        }

        [HttpGet]
        public Task<ActionResult> GetById(int id)
            => operationMediator.Execute(new GetInvoiceByIdQuery { Id = id }).OkOrNotFound();

        [HttpPost]
        public Task<ActionResult> Create(CreateInvoiceCommand command)
            => operationMediator.Execute(command).Ok();
    }
}
