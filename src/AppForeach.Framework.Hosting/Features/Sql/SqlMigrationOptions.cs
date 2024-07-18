using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlMigrationOptions<TDbContext>
        where TDbContext : DbContext
    {
        public string? ConnectionString { get; set; }

        public Action<SqlServerDbContextOptionsBuilder>? DbContextOptions { get; set; }
    }
}
