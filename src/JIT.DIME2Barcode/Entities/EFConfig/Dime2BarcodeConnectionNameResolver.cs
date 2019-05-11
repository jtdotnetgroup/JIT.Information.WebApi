using System;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using JIT.InfomationSystem.Configuration;
using JIT.InfomationSystem.Web;
using Microsoft.Extensions.Configuration;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class Dime2BarcodeConnectionNameResolver:DefaultConnectionStringResolver
    {
        public Dime2BarcodeConnectionNameResolver(IAbpStartupConfiguration configuration) : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var type = args["DbContextConcreteType"] as Type;
            if (type == typeof(Dime2barcodeContext))
            {
                var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
                return configuration.GetConnectionString("DIME2Barcode");
            }

            return base.GetNameOrConnectionString(args);
        }
    }
}