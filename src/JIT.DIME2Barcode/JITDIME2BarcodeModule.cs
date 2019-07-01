using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Entities.EFConfig;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using System;
using System.Reflection;
using Abp.Dependency;
using JIT.DIME2Barcode.BackgroudJobs;
using JIT.DIME2Barcode.BaseData.Equipment.Dtos;
using JIT.DIME2Barcode.SystemSetting.Employee.Dtos;
using JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos;

namespace JIT.DIME2Barcode
{
    public class JITDIME2BarcodeModule:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(1);
            });


            Configuration.Authorization.Providers.Add<ProductionPlanPermissionProvider>();

            Configuration.ReplaceService<IConnectionStringResolver, Dime2BarcodeConnectionNameResolver>();

            

            ConfigurDbContext();

            ConfigurAutoMapper();
            ConfigurSyncJobs();

            //设置缓存
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });

            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        protected void ConfigurDbContext()
        {
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
                config.CreateMap<t_OrganizationUnit, OrganizationDto>()
                    .ForMember(o => o.Children, option => option.Ignore());
        
            });
        }

        protected void ConfigurAutoMapper()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<OrganizationCreateInput, t_OrganizationUnit>()
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

                config.CreateMap<t_OrganizationUnit, OrganizationDtoTest>()
                    .ForMember(o => o.title, op => op.MapFrom(input => input.DisplayName))
                    .ForMember(o => o.key, op => op.MapFrom(input => input.Code))
                    .ForMember(o => o.value, op => op.Ignore())
                    .ForMember(o => o.label, op => op.Ignore());


                config.CreateMap<EquipmentDto, Equipment>()
                    .ForMember(o => o.CreationTime, op => op.Ignore())
                    .ForMember(o => o.CreatorUserId, op => op.Ignore())
                    .ForMember(o => o.DeleterUserId, op => op.Ignore())
                    .ForMember(o => o.DeletionTime, op => op.Ignore())
                    .ForMember(o => o.Id, op => op.Ignore());

                config.CreateMap<Equipment, EquipmentDto>()
                    .ForMember(o => o.WorkCenter, op => op.MapFrom(input=>input.WorkCenter.DisplayName));

                config.CreateMap<EquipmentShiftDto, EqiupmentShift>()
                    .ForMember(o => o.Employee, op => op.Ignore())
                    .ForMember(o => o.Equipment, op => op.Ignore());

                config.CreateMap<EmployeeDto, Employee>()
                    .ForMember(o => o.Department, op => op.Ignore());

                config.CreateMap<EmployeeEdit, Employee>()
                    .ForMember(o => o.Department, op => op.Ignore());

                config.CreateMap<ICMODispBill, ICMODispBillRecord>()
                    .ForMember(o => o.CreateTime, op => op.Ignore())
                    .ForMember(o => o.CreateUserID, op => op.Ignore());

                config.CreateMap<TB_BadItemRelation, TB_BadItemRelationDto>()
                    .ForMember(o => o.FItemID, op => op.Ignore())
                    .ForMember(o => o.FOperName, op => op.Ignore()); 

            });

            }

        protected void ConfigurSyncJobs()
        {
            var success= IocManager.RegisterIfNot(typeof(SyncItemJob));
        }
        
    }
}
