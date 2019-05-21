using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JIT.InfomationSystem.Roles.Dto;
using JIT.InfomationSystem.Users.Dto;

namespace JIT.InfomationSystem.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
