using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using JIT.InfomationSystem.Configuration;
using JIT.InfomationSystem.Web;

namespace JIT.InfomationSystem.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class InfomationSystemDbContextFactory : IDesignTimeDbContextFactory<InfomationSystemDbContext>
    {
        public InfomationSystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InfomationSystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            InfomationSystemDbContextConfigurer.Configure(builder, configuration.GetConnectionString(InfomationSystemConsts.ConnectionStringName));

            return new InfomationSystemDbContext(builder.Options);
        }
    }
}
