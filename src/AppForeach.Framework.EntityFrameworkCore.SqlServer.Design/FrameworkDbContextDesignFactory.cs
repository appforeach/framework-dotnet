using AppForeach.Framework.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppForeach.Framework.EntityFrameworkCore.SqlServer.Design
{
    public class FrameworkDbContextDesignFactory : IDesignTimeDbContextFactory<FrameworkDbContext>
    {
        public FrameworkDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SqlExpress; Initial Catalog=appforeach; Integrated Security=true; MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False;",
                opt =>
                {
                    opt.MigrationsHistoryTable("__EFMigrationsHistory", FrameworkDbContext.Schema);
                    opt.MigrationsAssembly(typeof(SqlEntityFrameworkComponents).Assembly.FullName);
                });
            return new FrameworkDbContext(optionsBuilder.Options);
        }
    }
}
