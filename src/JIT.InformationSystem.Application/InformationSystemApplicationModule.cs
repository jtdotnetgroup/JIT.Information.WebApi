using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.InformationSystem.Authorization;

namespace JIT.InformationSystem
{
    [DependsOn(
        typeof(InformationSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class InformationSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<InformationSystemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(InformationSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
