using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    internal class DbContextInternalActivator : IDbContextInternalActivator
    {
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly IServiceProvider serviceProvider;

        public DbContextInternalActivator(IConnectionStringProvider connectionStringProvider, IServiceProvider serviceProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
            this.serviceProvider = serviceProvider;
        }
        public TDbContext Activate<TDbContext>() where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer(connectionStringProvider.ConnectionString);

            var db = (TDbContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDbContext), optionsBuilder.Options);

            return db;
        }
    }
}
