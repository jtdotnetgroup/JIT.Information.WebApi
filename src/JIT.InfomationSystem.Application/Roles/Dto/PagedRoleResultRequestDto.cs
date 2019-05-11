using Abp.Application.Services.Dto;

namespace JIT.InfomationSystem.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

