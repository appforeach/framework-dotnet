using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class FrameworkDbContext : DbContext
    {
        public const string Schema = "framework";

        public DbSet<TransactionEntity> Transactions { get; set; }

        public FrameworkDbContext()
        {
        }

        public FrameworkDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrameworkDbContext).Assembly);
        }
    }
}
