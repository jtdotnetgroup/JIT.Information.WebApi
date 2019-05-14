using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using JIT.DIME2Barcode;
using JIT.InfomationSystem.Configuration;
using Microsoft.AspNetCore.Cors;

namespace JIT.InfomationSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(InfomationSystemWebCoreModule),
       typeof(JITDIME2BarcodeModule)
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
