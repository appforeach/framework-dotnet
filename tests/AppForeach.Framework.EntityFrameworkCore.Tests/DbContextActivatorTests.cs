using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System.Data.Common;

namespace AppForeach.Framework.EntityFrameworkCore.Tests
{
    public class DbContextActivatorTests : IDisposable
    {
        private readonly Mock<IOperationContext> operationContextMock;
        private readonly Mock<IConnectionStringProvider> connectionStringProviderMock;
        private readonly Mock<IDbOptionsConfigurator> dbOptionsConfiguratorMock;
        private readonly Mock<IServiceProvider> serviceProviderMock;
        private readonly DbContextActivator activator;

        private OperationContextState? operationContextState;
        private TransactionScopeState? transactionScopeState;

        [Fact]
        public async Task Activate_Should_CreateEnlistedDbContext_WhenDefaultStrategyAndTransactionOpened()
        {
            await OpenTransaction();

            using var db = activator.Activate<TestDbContext>();

            db.Database.CurrentTransaction.ShouldNotBeNull();
        }

        [Fact]
        public async Task Activate_Should_CreateIndependentDbContext_WhenDefaultStrategyAndQuery()
        {
            SetQuery();

            using var db = activator.Activate<TestDbContext>();

            db.Database.CurrentTransaction.ShouldBeNull();
        }

        [Fact]
        public async Task Activate_Should_CreateEnlistedDbContext_WhenRequiredStrategyAndTransactionOpened()
        {
            await OpenTransaction();

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            db.Database.CurrentTransaction.ShouldNotBeNull();
        }

        [Fact]
        public async Task Activate_Should_CreateIndependentDbContext_WhenRequiredStrategyAndQuery()
        {
            SetQuery();

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            db.Database.CurrentTransaction.ShouldBeNull();
        }

        [Fact]
        public async Task Activate_Should_Throw_WhenRequiredStrategyAndNoOperationContext()
        {
            operationContextState = null;

            var act = () => activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            act.ShouldThrow<FrameworkException>();
        }

        [Fact]
        public async Task Activate_Should_Throw_WhenRequiredStrategyAndInputNotSet()
        {
            operationContextState = new OperationContextState
            {
                IsOperationInputSet = false
            };

            var act = () => activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            act.ShouldThrow<FrameworkException>();
        }

        [Fact]
        public async Task Activate_Should_Throw_WhenRequiredStrategyAndNoTransactionState()
        {
            transactionScopeState = null;

            var act = () => activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            act.ShouldThrow<FrameworkException>();
        }

        [Fact]
        public async Task Activate_Should_Throw_WhenRequiredStrategyAndTransactionNotSet()
        {
            transactionScopeState = new TransactionScopeState
            {
                IsTransactionInitialized = false
            };

            var act = () => activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Required);

            act.ShouldThrow<FrameworkException>();
        }

        [Fact]
        public async Task Activate_Should_CreateEnlistedDbContext_WhenOptionalStrategyAndTransactionOpened()
        {
            await OpenTransaction();

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Optional);

            db.Database.CurrentTransaction.ShouldNotBeNull();
        }

        [Fact]
        public async Task Activate_Should_CreateIndependentReadOnlyDbContext_WhenOptionalStrategyAndQuery()
        {
            SetQuery();

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Optional);

            db.Database.CurrentTransaction.ShouldBeNull();

            var act = () => db.SaveChangesAsync();

            await act.ShouldThrowAsync<FrameworkException>();
        }

        [Fact]
        public async Task Activate_Should_CreateIndependentModifiableDbContext_WhenOptionalStrategyAndNoOperation()
        {
            operationContextState = null;

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Optional);

            db.Database.CurrentTransaction.ShouldBeNull();

            await db.SaveChangesAsync();
        }

        [Fact]
        public async Task Activate_Should_CreateIndependentDbContext_WhenSuppressStrategyAndTransaction()
        {
            await OpenTransaction();

            using var db = activator.Activate<TestDbContext>(DbContextOperationEnlistmentStrategy.Suppress);

            db.Database.CurrentTransaction.ShouldBeNull();
        }

        public DbContextActivatorTests()
        {

            operationContextState = new OperationContextState
            {
                IsOperationInputSet = true,
                IsCommand = true
            };

            transactionScopeState = new TransactionScopeState
            {
                IsTransactionInitialized = true
            };

            operationContextMock = new Mock<IOperationContext>();
            operationContextMock.SetupGet(m => m.State).Returns(() =>
            {
                var state = new Bag();

                if (operationContextState != null)
                    state.Set(operationContextState);
                if (transactionScopeState != null)
                    state.Set(transactionScopeState);

                return state;
            });

            operationContextMock.SetupGet(m => m.IsCommand).Returns(() =>
            {
                return operationContextState?.IsCommand ?? false;
            });

            connectionStringProviderMock = new Mock<IConnectionStringProvider>();

            dbOptionsConfiguratorMock = new Mock<IDbOptionsConfigurator>();
            dbOptionsConfiguratorMock.Setup(m => m.SetConnectionString(It.IsAny<DbContextOptionsBuilder<TestDbContext>>(), It.IsAny<string>(),
                It.IsAny<TransactionRetrySettings>()))
                .Callback((DbContextOptionsBuilder<TestDbContext> dbBuilder, string connection, TransactionRetrySettings settings) =>
                {
                    dbBuilder.UseSqlite("DataSource=:memory:");
                });

            dbOptionsConfiguratorMock.Setup(m => m.SetConnection(It.IsAny<DbContextOptionsBuilder<TestDbContext>>(), It.IsAny<DbConnection>()))
                .Callback((DbContextOptionsBuilder<TestDbContext> dbBuilder, DbConnection connection) =>
                {
                    dbBuilder.UseSqlite(connection);
                });

            serviceProviderMock = new Mock<IServiceProvider>();

            activator = new DbContextActivator(operationContextMock.Object, connectionStringProviderMock.Object, dbOptionsConfiguratorMock.Object, serviceProviderMock.Object);
        }

        private async Task OpenTransaction()
        {
            DbContextOptionsBuilder<FrameworkDbContext> optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseSqlite("DataSource=:memory:");
            var db = new FrameworkDbContext(optionsBuilder.Options);

            transactionScopeState!.DbContext = db;
            transactionScopeState!.DbContextTransaction = await db.Database.BeginTransactionAsync();
        }

        private void SetQuery()
        {
            operationContextState!.IsCommand = false;
        }

        public void Dispose()
        {
            if(transactionScopeState != null && transactionScopeState.DbContextTransaction != null)
            {
                transactionScopeState.DbContextTransaction.Rollback();
                transactionScopeState.DbContextTransaction.Dispose();
            }
        }

        private class TestDbContext : DbContext
        {
            public TestDbContext(DbContextOptions options) : base(options)
            {
            }
        }
    }
}
