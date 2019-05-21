using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using JIT.InformationSystem.Configuration.Dto;

namespace JIT.InformationSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : InformationSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
