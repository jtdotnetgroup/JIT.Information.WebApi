using System.Threading.Tasks;
using Abp.Application.Services;
using JIT.InformationSystem.Authorization.Accounts.Dto;

namespace JIT.InformationSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
