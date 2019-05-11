using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace JIT.InfomationSystem.Controllers
{
    public abstract class InfomationSystemControllerBase: AbpController
    {
        protected InfomationSystemControllerBase()
        {
            LocalizationSourceName = InfomationSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
