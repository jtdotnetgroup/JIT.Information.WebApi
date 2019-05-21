using System.Threading.Tasks;
using JIT.InfomationSystem.Configuration.Dto;

namespace JIT.InfomationSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
