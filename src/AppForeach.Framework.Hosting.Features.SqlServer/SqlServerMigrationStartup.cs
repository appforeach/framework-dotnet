using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace AppForeach.Framework.Hosting.Features.SqlServer
{
    public class SqlServerMigrationStartup<TDbContext> : SqlMigrationStartup<TDbContext>
        where TDbContext : DbContext
    {
        private readonly IOptions<SqlServerMigrationOptions<TDbContext>> migrationOptions;

        public SqlServerMigrationStartup(IOptions<SqlServerMigrationOptions<TDbContext>> migrationOptions)
            : base(migrationOptions) 
        {
            this.migrationOptions = migrationOptions;
        }

        protected override void ConfigureDbOptions(DbContextOptionsBuilder<TDbContext> builder)
        {
            builder.UseSqlServer(migrationOptions.Value.ConnectionString, migrationOptions.Value.DbContextOptions);
        }
    }
}
