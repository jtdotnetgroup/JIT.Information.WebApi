using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using JIT.DIME2Barcode;
using JIT.InformationSystem.Authentication.JwtBearer;
using JIT.InformationSystem.Configuration;
using JIT.InformationSystem.EntityFrameworkCore;

namespace JIT.InformationSystem
{
    [DependsOn(
         typeof(InformationSystemApplicationModule),
         typeof(InformationSystemEntityFrameworkModule),
         typeof(AbpAspNetCoreModule),
         typeof(JITDIME2BarcodeModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class InformationSystemWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public InformationSystemWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                InformationSystemConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(InformationSystemApplicationModule).GetAssembly()
                 );

            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(JITDIME2BarcodeModule).GetAssembly());

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
            tokenAuthConfig.PrivateKey = _appConfiguration["RsaPrivatekey"];
            tokenAuthConfig.PublicKey = _appConfiguration["RsaPublickey"];
            tokenAuthConfig.ClientName = _appConfiguration["ClientName"];
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InformationSystemWebCoreModule).GetAssembly());
        }
    }
}
