using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public interface IDbContextInternalActivator
    {
        TDbContext Activate<TDbContext>() where TDbContext : DbContext;
    }
}
