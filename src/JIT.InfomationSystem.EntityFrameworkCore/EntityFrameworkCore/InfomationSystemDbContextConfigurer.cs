using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace JIT.InfomationSystem.EntityFrameworkCore
{
    public static class InfomationSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InfomationSystemDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InfomationSystemDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
