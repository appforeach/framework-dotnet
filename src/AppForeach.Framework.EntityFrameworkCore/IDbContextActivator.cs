
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public interface IDbContextActivator
    {
        TDbContext Activate<TDbContext>() where TDbContext : DbContext;
    }
}
