using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EscapeHit.App.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
