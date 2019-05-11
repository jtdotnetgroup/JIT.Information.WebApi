using System.Threading.Tasks;
using Abp.Application.Services;
using JIT.InfomationSystem.Sessions.Dto;

namespace JIT.InfomationSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
