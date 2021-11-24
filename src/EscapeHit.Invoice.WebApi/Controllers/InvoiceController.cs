using System.Threading.Tasks;
using AppForeach.Framework;
using EscapeHit.Invoice.Commands.CreateInvoice;
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
            var result = await operationMediator.Execute(new CreateInvoiceCommand()).As<CreateInvoiceResult>();

            return Ok("hey!");
        }
    }
}
