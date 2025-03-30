using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlMigrationOptions<TDbContext> : SqlMigrationOptions<TDbContext>
    where TDbContext : DbContext
{
    public Action<NpgsqlDbContextOptionsBuilder>? DbContextOptions { get; set; }
}
