using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscapeHit.App.Database.Design
{
    public class AppDbContextDesignFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(local); Initial Catalog=invoice; Integrated Security=true; MultipleActiveResultSets=True;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
