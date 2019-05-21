using Abp.Authorization;
using JIT.InformationSystem.Authorization.Roles;
using JIT.InformationSystem.Authorization.Users;

namespace JIT.InformationSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
