using AppForeach.Framework.Hosting.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public abstract class SqlMigrationStartup<TDbContext> : IApplicationStartup
        where TDbContext : DbContext
    {
        private readonly IOptions<SqlMigrationOptions<TDbContext>> migrationOptions;

        public SqlMigrationStartup(IOptions<SqlMigrationOptions<TDbContext>> migrationOptions)
        {
            this.migrationOptions = migrationOptions;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            ConfigureDbOptions(optionsBuilder);

            using (var dbContext = CreateDbContext(optionsBuilder.Options))
            {
                dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        protected abstract void ConfigureDbOptions(DbContextOptionsBuilder<TDbContext> builder);

        private TDbContext CreateDbContext(DbContextOptions<TDbContext> options) 
        {
            var factoryBaseType = typeof(IDesignTimeDbContextFactory<>).MakeGenericType(typeof(TDbContext));

            var dbContextDesignTimeFactoryType = typeof(TDbContext).Assembly.DefinedTypes
                .SingleOrDefault(t => !t.IsAbstract && !t.IsGenericTypeDefinition && factoryBaseType.IsAssignableFrom(t));

            if (dbContextDesignTimeFactoryType != null)
            {
                var dbContextDesignTimeFactory = Activator.CreateInstance(dbContextDesignTimeFactoryType) as IDesignTimeDbContextFactory<TDbContext>
                        ?? throw new FrameworkException("Could not activate " + nameof(IDesignTimeDbContextFactory<TDbContext>));
                return dbContextDesignTimeFactory.CreateDbContext(Array.Empty<string>());
            }
            else
            {
                return Activator.CreateInstance(typeof(TDbContext), options) as TDbContext
                    ?? throw new FrameworkException("Could not activate " + nameof(TDbContext));
            }
        }
    }
}
