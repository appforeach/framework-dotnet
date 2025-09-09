using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AppForeach.Framework.EntityFrameworkCore.SqlServer;

public class SqlDbOptions
{
    public Action<SqlServerDbContextOptionsBuilder>? DbOptions { get; set; } 
}
