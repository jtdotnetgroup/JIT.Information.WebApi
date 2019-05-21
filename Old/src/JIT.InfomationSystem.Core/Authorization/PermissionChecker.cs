using Abp.Authorization;
using JIT.InfomationSystem.Authorization.Roles;
using JIT.InfomationSystem.Authorization.Users;

namespace JIT.InfomationSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
