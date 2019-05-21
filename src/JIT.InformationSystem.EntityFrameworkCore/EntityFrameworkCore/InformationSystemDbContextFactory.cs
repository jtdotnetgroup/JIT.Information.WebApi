using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using JIT.InformationSystem.Configuration;
using JIT.InformationSystem.Web;

namespace JIT.InformationSystem.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class InformationSystemDbContextFactory : IDesignTimeDbContextFactory<InformationSystemDbContext>
    {
        public InformationSystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InformationSystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            InformationSystemDbContextConfigurer.Configure(builder, configuration.GetConnectionString(InformationSystemConsts.ConnectionStringName));

            return new InformationSystemDbContext(builder.Options);
        }
    }
}
