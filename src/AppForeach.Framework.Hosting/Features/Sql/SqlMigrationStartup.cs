using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.EntityFrameworkCore;
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
    public class SqlMigrationStartup<TDbContext> : IApplicationStartup
        where TDbContext : DbContext
    {
        private readonly IOptions<SqlMigrationOptions<TDbContext>> migrationOptions;
        private readonly IServiceLocator serviceLocator;

        public SqlMigrationStartup(IOptions<SqlMigrationOptions<TDbContext>> migrationOptions, IServiceLocator serviceLocator)
        {
            this.migrationOptions = migrationOptions;
            this.serviceLocator = serviceLocator;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer(migrationOptions.Value.ConnectionString, migrationOptions.Value.DbContextOptions);

            using (var dbContext = CreateDbContext(optionsBuilder.Options, serviceLocator))
            {
                dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        private TDbContext CreateDbContext(DbContextOptions<TDbContext> options, IServiceLocator serviceLocator)
        {
            if (typeof(TDbContext) == typeof(FrameworkDbContext))
            {
                // service locator is not needed out there
                return (Activator.CreateInstance(typeof(TDbContext), options) as TDbContext)
                    ?? throw new FrameworkException("Could not activate " + nameof(TDbContext));
            }

            return (Activator.CreateInstance(typeof(TDbContext), options, serviceLocator) as TDbContext)
                ?? throw new FrameworkException("Could not activate " + nameof(TDbContext));

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
