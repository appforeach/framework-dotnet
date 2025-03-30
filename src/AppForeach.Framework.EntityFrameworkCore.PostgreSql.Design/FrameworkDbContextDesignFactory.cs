using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql.Design
{
    public class FrameworkDbContextDesignFactory : IDesignTimeDbContextFactory<FrameworkDbContext>
    {
        public FrameworkDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=invoice;User Id=appforeach;Password=pass@word1;",
                opt =>
                {
                    opt.MigrationsHistoryTable("__EFMigrationsHistory", FrameworkDbContext.Schema);
                    opt.MigrationsAssembly(typeof(PostgreSqlEntityFrameworkComponents).Assembly.FullName);
                });
            return new FrameworkDbContext(optionsBuilder.Options);
        }
    }
}
