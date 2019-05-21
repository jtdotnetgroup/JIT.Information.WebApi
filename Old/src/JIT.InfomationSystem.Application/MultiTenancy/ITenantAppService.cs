using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JIT.InfomationSystem.MultiTenancy.Dto;

namespace JIT.InfomationSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

