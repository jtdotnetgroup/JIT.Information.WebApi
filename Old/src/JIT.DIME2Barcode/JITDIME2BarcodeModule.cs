using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Organizations;
using Abp.Reflection.Extensions;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using System.Reflection;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Entities.EFConfig;

namespace JIT.DIME2Barcode
{
    public class JITDIME2BarcodeModule:AbpModule
    {
        public override void PreInitialize()
        {

            Configuration.ReplaceService<IConnectionStringResolver, Dime2BarcodeConnectionNameResolver>();

            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(JITDIME2BarcodeModule).GetAssembly());

            Configuration.Modules.AbpEfCore().AddDbContext<Dime2barcodeContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    Dime2BarcodeContextConfig.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    Dime2BarcodeContextConfig.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });


            Configuration.Modules.AbpEfCore().AddDbContext<ProductionPlanMySqlDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    Dime2BarcodeContextConfig.ConfigureMySql(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    Dime2BarcodeContextConfig.ConfigureMySql(options.DbContextOptions, options.ConnectionString);
                }
            });

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<OrganizationCreateInput, OrganizationUnit>()
                    .ForMember(o => o.Parent, option => option.Ignore())
                    .ForMember(o => o.Children, option => option.Ignore())
                    .ForMember(o => o.IsDeleted, option => option.Ignore())
                    .ForMember(o => o.DeleterUserId, option => option.Ignore())
                    .ForMember(o => o.DeletionTime, option => option.Ignore())
                    .ForMember(o => o.LastModificationTime, option => option.Ignore())
                    .ForMember(o => o.LastModifierUserId, op => op.Ignore())
                    .ForMember(o => o.CreationTime, op => op.Ignore())
                    .ForMember(o => o.CreatorUserId, op => op.Ignore())
                    .ForMember(o => o.Id, op => op.Ignore());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
