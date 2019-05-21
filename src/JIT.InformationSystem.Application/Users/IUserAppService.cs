using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JIT.InformationSystem.Roles.Dto;
using JIT.InformationSystem.Users.Dto;

namespace JIT.InformationSystem.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
