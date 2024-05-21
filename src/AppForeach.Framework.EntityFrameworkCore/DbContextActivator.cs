
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class DbContextActivator : IDbContextActivator
    {
        private readonly IOperationContext operationContext;
        private readonly IDbContextInternalActivator dbContextInternalActivator;
        private readonly IServiceProvider serviceProvider;

        public DbContextActivator(IOperationContext operationContext, IServiceProvider serviceProvider,
            IDbContextInternalActivator dbContextInternalActivator)
        {
            this.operationContext = operationContext;
            this.serviceProvider = serviceProvider;
            this.dbContextInternalActivator = dbContextInternalActivator;
        }

        public TDbContext Activate<TDbContext>() where TDbContext : DbContext
        {
            EnsureDbContextCanBeActivated();

            TDbContext db;

            if (operationContext.IsCommand)
            {
                var transationState = operationContext.State.Get<TransactionScopeState>();

                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                optionsBuilder.UseSqlServer(transationState.DbContext.Database.GetDbConnection());

                db = (TDbContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDbContext), optionsBuilder.Options);

                db.Database.UseTransaction(transationState.DbContextTransaction.GetDbTransaction());
            }
            else
            {
                db = dbContextInternalActivator.Activate<TDbContext>();

                db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                db.SavingChanges += Db_SavingChanges;
            }

            return db;
        }

        private void EnsureDbContextCanBeActivated()
        {
            var operationContextState = operationContext.State.Get<OperationContextState>();
            if(!operationContextState.IsOperationInputSet)
            {
                throw new FrameworkException("DbContext cannot be activated outside of mediator execution context.");
            }

            var transationState = operationContext.State.Get<TransactionScopeState>();
            if(!transationState.IsTransactionInitialized)
            {
                throw new FrameworkException("DbContext cannot be activated outside of transaction middleware.");
            }
        }

        private void Db_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            throw new FrameworkException("SaveChanges is invalid in context of Query, use Command instead.");
        }
    }
}
