
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public interface IDbContextActivator
    {
        TDbContext Activate<TDbContext>(DbContextOperationEnlistmentStrategy operationEnlistmentStrategy = DbContextOperationEnlistmentStrategy.Required) where TDbContext : DbContext;
    }
}
