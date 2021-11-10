using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class TransactionScopeState
    {
        public DbContext DbContext { get; set; }

        public IDbContextTransaction DbContextTransaction { get;set; }
    }
}
