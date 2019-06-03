using Abp.Authorization;
using Abp.Localization;
using JIT.InformationSystem;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.Permissions
{
    public class ProductionPlanPermissionProvider: AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            PermissionSeter.SetPermission<ProductionPlanPermissionsNames>(context);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, "InformationSystem");
        }
    }
}