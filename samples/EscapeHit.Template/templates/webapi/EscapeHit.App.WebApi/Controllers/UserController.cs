using System.Threading.Tasks;
using AppForeach.Framework;
using EscapeHit.App.Commands.CreateUser;
using EscapeHit.App.Queries.GetUserById;
using EscapeHit.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.App.WebApi.Controllers
{
    public class UserController : WebApiController
    {
        private readonly IOperationMediator operationMediator;

        public UserController(IOperationMediator operationMediator)
        {
            this.operationMediator = operationMediator;
        }

        [HttpGet]
        public Task<ActionResult> GetById(int id)
            => operationMediator.Execute(new GetUserByIdQuery { Id = id }).OkOrNotFound();

        [HttpPost]
        public Task<ActionResult> Create(CreateUserCommand command)
            => operationMediator.Execute(command).Ok();
    }
}
