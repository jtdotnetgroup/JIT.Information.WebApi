using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class Dime2BarcodeContextConfig
    {
        public static void Configure(DbContextOptionsBuilder<Dime2barcodeContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<Dime2barcodeContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}