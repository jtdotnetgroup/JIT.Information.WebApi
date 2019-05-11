using System;
using System.Reflection;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Entities.EFConfig;
using Microsoft.EntityFrameworkCore.Storage;

namespace JIT.DIME2Barcode
{
    public class JITDIME2BarcodeModule:AbpModule
    {
        public override void PreInitialize()
        {

            Configuration.ReplaceService<IConnectionStringResolver,Dime2BarcodeConnectionNameResolver>();

            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(JITDIME2BarcodeModule).GetAssembly());

            Configuration.Modules.AbpEfCore().AddDbContext<Dime2barcodeContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    Dime2BarcodeContextConfig.Configure(options.DbContextOptions,options.ExistingConnection);
                }
                else
                {
                    Dime2BarcodeContextConfig.Configure(options.DbContextOptions,options.ConnectionString);
                }
            });

            IocManager.Register<Dime2barcodeContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
