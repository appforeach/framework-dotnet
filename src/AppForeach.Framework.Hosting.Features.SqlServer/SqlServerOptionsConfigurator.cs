using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.SqlServer
{
    public class SqlServerOptionsConfigurator<TDbContext>
        (
            SqlServerFeatureOption<TDbContext> option
        )
        : SqlOptionsConfigurator<TDbContext>(option), ISqlServerOptionsConfigurator
        where TDbContext : DbContext
    {
    }
}
