using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppForeach.Framework.EntityFrameworkCore.Design
{
    public class FrameworkDbContextDesignFactory : IDesignTimeDbContextFactory<FrameworkDbContext>
    {
        public FrameworkDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
            optionsBuilder.UseSqlServer("Server=(local); Initial Catalog=invoice; Integrated Security=true; MultipleActiveResultSets=True;",
                opt =>
                {
                    opt.MigrationsHistoryTable("__EFMigrationsHistory", FrameworkDbContext.Schema);
                });
            return new FrameworkDbContext(optionsBuilder.Options);
        }
    }
}
