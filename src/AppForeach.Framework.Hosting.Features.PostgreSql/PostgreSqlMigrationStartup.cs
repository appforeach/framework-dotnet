using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlMigrationStartup<TDbContext> : SqlMigrationStartup<TDbContext>
    where TDbContext : DbContext
{
    private readonly IOptions<PostgreSqlMigrationOptions<TDbContext>> migrationOptions;

    public PostgreSqlMigrationStartup(IOptions<PostgreSqlMigrationOptions<TDbContext>> migrationOptions)
        : base(migrationOptions) 
    {
        this.migrationOptions = migrationOptions;
    }

    protected override void ConfigureDbOptions(DbContextOptionsBuilder<TDbContext> builder)
    {
        builder.UseNpgsql(migrationOptions.Value.ConnectionString, migrationOptions.Value.DbContextOptions);
    }
}
