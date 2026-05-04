
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class DbContextActivator : IDbContextActivator
    {
        private readonly IOperationContext operationContext;
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly IDbOptionsConfigurator dbOptionsConfigurator;
        private readonly IServiceProvider serviceProvider;

        public DbContextActivator(IOperationContext operationContext, IConnectionStringProvider connectionStringProvider,
            IDbOptionsConfigurator dbOptionsConfigurator, IServiceProvider serviceProvider)
        {
            this.operationContext = operationContext;
            this.connectionStringProvider = connectionStringProvider;
            this.dbOptionsConfigurator = dbOptionsConfigurator;
            this.serviceProvider = serviceProvider;
        }

        public TDbContext Activate<TDbContext>(DbContextOperationEnlistmentStrategy operationEnlistmentStrategy = DbContextOperationEnlistmentStrategy.Required) where TDbContext : DbContext
        {
            bool? isCommand = GetIsCurrentOperationCommand(operationEnlistmentStrategy == DbContextOperationEnlistmentStrategy.Required);

            TDbContext db;

            if (isCommand == true && operationEnlistmentStrategy != DbContextOperationEnlistmentStrategy.Suppress)
            {
                var transationState = operationContext.State.Get<TransactionScopeState>();

                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                dbOptionsConfigurator.SetConnection(optionsBuilder, transationState.DbContext.Database.GetDbConnection());

                db = (TDbContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDbContext), optionsBuilder.Options);

                db.Database.UseTransaction(transationState.DbContextTransaction.GetDbTransaction());
            }
            else
            {
                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                dbOptionsConfigurator.SetConnectionString(optionsBuilder, connectionStringProvider.ConnectionString);

                db = (TDbContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDbContext), optionsBuilder.Options);

                if (isCommand == false)
                {
                    db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    db.SavingChanges += Db_SavingChanges;
                }
            }

            return db;
        }

        private bool? GetIsCurrentOperationCommand(bool isOperationMandatory)
        {
            var operationContextState = operationContext.State.Get<OperationContextState>();
            if(!operationContextState.IsOperationInputSet)
            {
                if (isOperationMandatory)
                {
                    throw new FrameworkException("DbContext cannot be activated outside of mediator execution context.");
                }
                else
                {
                    return null;
                }
            }


            var transationState = operationContext.State.Get<TransactionScopeState>();
            if (!transationState.IsTransactionInitialized)
            {
                if (isOperationMandatory)
                {
                    throw new FrameworkException("DbContext cannot be activated outside of transaction middleware.");
                }
                else
                {
                    return null;
                }
            }

            return operationContext.IsCommand;
        }

        private void Db_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            throw new FrameworkException("SaveChanges is invalid in context of Query, use Command instead.");
        }
    }
}
