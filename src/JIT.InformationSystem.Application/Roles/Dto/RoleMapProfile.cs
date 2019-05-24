using AutoMapper;
using Abp.Authorization;
using Abp.Authorization.Roles;
using JIT.InformationSystem.Authorization.Roles;

namespace JIT.InformationSystem.Roles.Dto
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            // Role and permission
            CreateMap<Abp.Authorization.Permission, string>().ConvertUsing(r => r.Name);
            CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

            CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
            CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
        }
    }
}
