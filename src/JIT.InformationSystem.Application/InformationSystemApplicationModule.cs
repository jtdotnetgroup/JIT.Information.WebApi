using System;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.InformationSystem.Authorization;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.Users.Dto;

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

            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });
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
