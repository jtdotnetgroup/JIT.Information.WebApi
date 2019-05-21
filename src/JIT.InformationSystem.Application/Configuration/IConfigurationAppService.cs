using System.Threading.Tasks;
using JIT.InformationSystem.Configuration.Dto;

namespace JIT.InformationSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
