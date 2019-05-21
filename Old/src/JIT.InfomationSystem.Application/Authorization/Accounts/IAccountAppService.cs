using System.Threading.Tasks;
using Abp.Application.Services;
using JIT.InfomationSystem.Authorization.Accounts.Dto;

namespace JIT.InfomationSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
