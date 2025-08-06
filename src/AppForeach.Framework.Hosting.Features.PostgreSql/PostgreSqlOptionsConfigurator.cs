using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.PostgreSql
{
    public class PostgreSqlOptionsConfigurator<TDbContext> 
        (
            PostgreSqlFeatureOption<TDbContext> option
        )
        : SqlOptionsConfigurator<TDbContext>(option), IPostgreSqlOptionsConfigurator
        where TDbContext : DbContext
    {
    }
}
