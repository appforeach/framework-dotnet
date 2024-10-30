using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.SqlServer
{
    public class SqlServerMigrationOptions<TDbContext> : SqlMigrationOptions<TDbContext>
        where TDbContext : DbContext
    {
        public Action<SqlServerDbContextOptionsBuilder>? DbContextOptions { get; set; }
    }
}
