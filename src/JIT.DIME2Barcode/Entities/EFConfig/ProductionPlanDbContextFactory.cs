using JIT.InformationSystem;
using JIT.InformationSystem.Configuration;
using JIT.InformationSystem.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class InformationSystemDbContextFactory : IDesignTimeDbContextFactory<ProductionPlanMySqlDbContext>
    {
        public ProductionPlanMySqlDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProductionPlanMySqlDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            Dime2BarcodeContextConfig.ConfigureMySql(builder, configuration.GetConnectionString(InformationSystemConsts.ConnectionStringName));

            return new ProductionPlanMySqlDbContext(builder.Options);
        }
    }
}