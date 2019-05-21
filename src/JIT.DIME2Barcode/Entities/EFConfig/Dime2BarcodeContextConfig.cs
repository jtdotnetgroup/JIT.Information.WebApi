using System.Data.Common;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class Dime2BarcodeContextConfig
    {
        public static void Configure<TDbContext>(DbContextOptionsBuilder<TDbContext> builder, string connectionString) where TDbContext:AbpDbContext
        {
            //builder.UseSqlServer(connectionString);
            //支持2005数据库分页
            builder.UseSqlServer(connectionString, b => b.UseRowNumberForPaging());
        }

        public static void Configure<TDbContext>(DbContextOptionsBuilder<TDbContext> builder, DbConnection connection) where TDbContext : AbpDbContext
        {
            //builder.UseSqlServer(connection);
            //支持2005数据库分页
            builder.UseSqlServer(connection, b =>
            {
                b.UseRowNumberForPaging();
            });
        }

        public static void ConfigureMySql<TDbContext>(DbContextOptionsBuilder<TDbContext> builder, string connectionString) where TDbContext : AbpDbContext
        {
            builder.UseMySql(connectionString);
        }

        public static void ConfigureMySql<TDbContext>(DbContextOptionsBuilder<TDbContext> builder, DbConnection connection) where TDbContext : AbpDbContext
        {
            builder.UseMySql(connection);
        }
    }
}