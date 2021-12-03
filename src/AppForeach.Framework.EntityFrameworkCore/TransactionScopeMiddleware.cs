using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class TransactionScopeMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly IConnectionStringProvider connectionStringProvider;

        public TransactionScopeMiddleware(IOperationContext context, IConnectionStringProvider connectionStringProvider)
        {
            this.context = context;
            this.connectionStringProvider = connectionStringProvider;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var scopeState = context.State.Get<TransactionScopeState>();

            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseSqlServer(connectionStringProvider.ConnectionString);

            using (var frameworkDb = new FrameworkDbContext(optionsBuilder.Options))
            using (var dbTransaction = await frameworkDb.Database.BeginTransactionAsync())
            {
                scopeState.DbContext = frameworkDb;


                scopeState.DbContextTransaction = dbTransaction;

                var transaction = new TransactionEntity();
                transaction.Name = "some_name";
                transaction.OccuredOn = DateTimeOffset.UtcNow;
                frameworkDb.Transactions.Add(transaction);

                await frameworkDb.SaveChangesAsync();

                await next();

                await dbTransaction.CommitAsync();
            }
        }
    }
}
