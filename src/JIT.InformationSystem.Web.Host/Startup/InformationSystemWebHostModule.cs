using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.InformationSystem.Configuration;

namespace JIT.InformationSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(InformationSystemWebCoreModule))]
    public class InformationSystemWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public InformationSystemWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InformationSystemWebHostModule).GetAssembly());
        }
    }
}
