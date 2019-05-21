using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace JIT.InformationSystem.Controllers
{
    public abstract class InformationSystemControllerBase: AbpController
    {
        protected InformationSystemControllerBase()
        {
            LocalizationSourceName = InformationSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
