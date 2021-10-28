using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EscapeHit.Invoice.WebApi
{
    public interface IUseCase
    {
        Task Execute();
    }

    public class UseCase : IUseCase
    {
        private readonly ILogger<UseCase> logger;
        private readonly IScopedService scopedService;
        private readonly ISingletonService singletonService;

        public UseCase(ILogger<UseCase> logger, IScopedService scopedService, ISingletonService singletonService)
        {
            this.logger = logger;
            this.scopedService = scopedService;
            this.singletonService = singletonService;
        }

        public async Task Execute()
        {
            logger.LogInformation($"use case - scoped { await scopedService.GetValue() }; singleton { await singletonService.GetValue() }");
        }
    }

    public interface IScopedService
    {
        Task<string> GetValue();
    }

    public class ScopedService : IScopedService
    {
        private readonly string val;
        public ScopedService()
        {
            val = Guid.NewGuid().ToString("n");
        }

        public Task<string> GetValue()
        {
            return Task.FromResult(val);
        }
    }

    public interface ISingletonService
    {
        Task<string> GetValue();
    }

    public class SingletonService : ISingletonService
    {
        private readonly string val;
        public SingletonService()
        {
            val = Guid.NewGuid().ToString("n");
        }

        public Task<string> GetValue()
        {
            return Task.FromResult(val);
        }
    }


}
