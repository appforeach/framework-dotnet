using System;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using IsolationLevel = System.Data.IsolationLevel;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class TransactionScopeMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly IDbOptionsConfigurator dbOptionsConfigurator;

        public TransactionScopeMiddleware(IOperationContext context, IConnectionStringProvider connectionStringProvider, IDbOptionsConfigurator dbOptionsConfigurator)
        {
            this.context = context;
            this.connectionStringProvider = connectionStringProvider;
            this.dbOptionsConfigurator = dbOptionsConfigurator;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var scopeState = context.State.Get<TransactionScopeState>();

            var retryFacet = context.Configuration.TryGet<TransactionRetryFacet>();
            var retryCountFacet = context.Configuration.TryGet<TransactionRetryCountFacet>();
            var retryDelayFacet = context.Configuration.TryGet<TransactionMaxRetryDelayFacet>();

            var retrySettings = new TransactionRetrySettings();
            retrySettings.Retry = retryFacet?.Retry ?? false;
            retrySettings.RetryCount = retryCountFacet?.RetryCount ?? 3;
            retrySettings.RetryDelay = retryDelayFacet?.MaxRetryDelay ?? TimeSpan.FromSeconds(30);

            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            dbOptionsConfigurator.SetConnectionString(optionsBuilder, connectionStringProvider.ConnectionString, retrySettings);
            
            using (var frameworkDb = new FrameworkDbContext(optionsBuilder.Options))
            {
                var strategy = frameworkDb.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var isolationLevelFacet = context.Configuration.TryGet<TransactionIsolationLevelFacet>();
                    IsolationLevel isolationLevel = isolationLevelFacet?.IsolationLevel ?? IsolationLevel.ReadCommitted;
                    await using var dbTransaction = await frameworkDb.Database.BeginTransactionAsync(isolationLevel);
                    scopeState.DbContext = frameworkDb;
                    scopeState.DbContextTransaction = dbTransaction;

                    if (context.IsCommand)
                    {
                        var transaction = new TransactionEntity();
                        transaction.Name = context.OperationName;
                        transaction.OccuredOn = DateTimeOffset.UtcNow;
                        frameworkDb.Transactions.Add(transaction);

                        await frameworkDb.SaveChangesAsync();

                        scopeState.TransactionId = transaction.Id;
                    }

                    scopeState.IsTransactionInitialized = true;

                    await next();

                    if (context.IsCommand)
                    {
                        await dbTransaction.CommitAsync();
                    }
                });
            }
        }
    }
}
