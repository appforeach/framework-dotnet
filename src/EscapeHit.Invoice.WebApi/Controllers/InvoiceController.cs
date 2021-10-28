using System.Threading.Tasks;
using EscapeHit.Invoice.Commands.CreateInvoice;
using EscapeHit.Invoice.Queries.GetInvoiceById;
using EscapeHit.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EscapeHit.Invoice.WebApi.Controllers
{
    public class InvoiceController : WebApiController
    {
        private readonly IOperationMediator operationMediator;
        private readonly ILogger<InvoiceController> logger;
        private readonly IUseCase useCase;
        private readonly IScopedService scopedService;
        private readonly ISingletonService singletonService;

        public InvoiceController(ILogger<InvoiceController> logger, IUseCase useCase, IScopedService scopedService, 
            ISingletonService singletonService/*IOperationMediator operationMediator*/)
        {
            this.logger = logger;
            this.useCase = useCase;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
            //this.operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ObjectResult> GetById(int id)
        {
            logger.LogInformation($"controller - scoped { await scopedService.GetValue() }; singleton { await singletonService.GetValue() }");

            await useCase.Execute();

            return Ok("hey!");
            //return await operationMediator.Execute(new GetInvoiceByIdQuery { Id = id }).OkOrNotFound();
        }

        [HttpPost]
        public async Task<ObjectResult> Create(CreateInvoiceCommand input)
        {
            return await operationMediator.Execute(input).Ok();
        }
    }
}
