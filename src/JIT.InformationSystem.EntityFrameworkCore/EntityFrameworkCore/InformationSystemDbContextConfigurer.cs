using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace JIT.InformationSystem.EntityFrameworkCore
{
    public static class InformationSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InformationSystemDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InformationSystemDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
