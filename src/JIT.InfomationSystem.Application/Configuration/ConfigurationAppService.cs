using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using JIT.InfomationSystem.Configuration.Dto;

namespace JIT.InfomationSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : InfomationSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
