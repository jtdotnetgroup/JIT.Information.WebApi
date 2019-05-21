using System.Threading.Tasks;
using Abp.Application.Services;
using JIT.InformationSystem.Sessions.Dto;

namespace JIT.InformationSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
