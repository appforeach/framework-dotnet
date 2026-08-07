using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore;

public class FrameworkDbContextFactory<TContext> 
    (
    IDbContextActivator activator
    ) : IDbContextFactory<TContext>
    where TContext : DbContext
{
    public TContext CreateDbContext()
        => activator.Activate<TContext>(DbContextOperationEnlistmentStrategy.Optional);
}
