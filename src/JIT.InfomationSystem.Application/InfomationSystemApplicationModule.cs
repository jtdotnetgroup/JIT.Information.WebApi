using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.InfomationSystem.Authorization;

namespace JIT.InfomationSystem
{
    [DependsOn(
        typeof(InfomationSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class InfomationSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<InfomationSystemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(InfomationSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
