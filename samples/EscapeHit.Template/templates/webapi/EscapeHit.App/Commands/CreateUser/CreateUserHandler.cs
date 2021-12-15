using System.Threading.Tasks;
using EscapeHit.App.Repositories;
using EscapeHit.App.Services;

namespace EscapeHit.App.Commands.CreateUser
{
    public class CreateUserHandler
    {
        private readonly ICreateUserInputMapping inputMapping;
        private readonly IUserRepository invoiceRepository;
        private readonly IUserNumberService invoiceNumberService;
        private readonly ICreateUserOutputMapping outputMapping;

        public CreateUserHandler(ICreateUserInputMapping inputMapping,
            IUserRepository invoiceRepository,
            IUserNumberService invoiceNumberService,
            ICreateUserOutputMapping outputMapping)
        {
            this.inputMapping = inputMapping;
            this.invoiceRepository = invoiceRepository;
            this.invoiceNumberService = invoiceNumberService;
            this.outputMapping = outputMapping;
        }

        public async Task<CreateUserResult> Execute(CreateUserCommand input)
        {
            var entity = inputMapping.MapFrom(input);

            entity.Number = invoiceNumberService.GenerateUserNumber(entity);

            await invoiceRepository.Create(entity);

            return outputMapping.MapFrom(entity);
        }
    }
}
