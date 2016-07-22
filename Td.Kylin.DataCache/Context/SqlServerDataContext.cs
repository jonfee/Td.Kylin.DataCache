using Microsoft.EntityFrameworkCore;

namespace Td.Kylin.DataCache.Context
{
    internal sealed class SqlServerDataContext : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(Startup.SqlConnctionString);
        }
    }
}
