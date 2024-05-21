using System;
using System.Collections.Generic;
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
        private readonly IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers;

        public TransactionScopeMiddleware(IOperationContext context, IConnectionStringProvider connectionStringProvider, IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers)
        {
            this.context = context;
            this.connectionStringProvider = connectionStringProvider;
            this.retryExceptionHandlers = retryExceptionHandlers;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var scopeState = context.State.Get<TransactionScopeState>();

            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseSqlServer(connectionStringProvider.ConnectionString, sqlOpt =>
            {
                var retryFacet = context.Configuration.TryGet<TransactionRetryFacet>();

                if(retryFacet?.Retry ?? false)
                {
                    var retryCountFacet = context.Configuration.TryGet<TransactionRetryCountFacet>();
                    var retryDelayFacet = context.Configuration.TryGet<TransactionMaxRetryDelayFacet>();

                    int maxRetry = retryCountFacet?.RetryCount ?? 3;
                    TimeSpan retryDelay = retryDelayFacet?.MaxRetryDelay ?? TimeSpan.FromSeconds(30);
                    sqlOpt.ExecutionStrategy(dp => new CustomSqlServerRetryingExecutionStrategy(dp, maxRetry, retryDelay, retryExceptionHandlers));
                }
            });
            
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
