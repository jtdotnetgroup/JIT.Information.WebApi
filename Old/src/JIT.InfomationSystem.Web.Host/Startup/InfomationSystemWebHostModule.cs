using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.InfomationSystem.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JIT.InfomationSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(InfomationSystemWebCoreModule)
       )]
    public class InfomationSystemWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public InfomationSystemWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(typeof(InfomationSystemWebHostModule).GetAssembly());
        }
    }
}
