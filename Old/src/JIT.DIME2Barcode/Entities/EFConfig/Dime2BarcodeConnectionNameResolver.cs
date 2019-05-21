using System;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using JIT.InfomationSystem.Configuration;
using JIT.InfomationSystem.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class Dime2BarcodeConnectionNameResolver:DefaultConnectionStringResolver
    {
        public IHostingEnvironment env { get; set; }

        public Dime2BarcodeConnectionNameResolver(IAbpStartupConfiguration configuration) : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var type = args["DbContextConcreteType"] as Type;
            var configuration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());

            if (type == typeof(Dime2barcodeContext))
            {
               
                return configuration.GetConnectionString("DIME2Barcode");
            }

            if (type == typeof(ProductionPlanMySqlDbContext))
            {
                return configuration.GetConnectionString("Default");
            }

            return base.GetNameOrConnectionString(args);
        }
    }
}