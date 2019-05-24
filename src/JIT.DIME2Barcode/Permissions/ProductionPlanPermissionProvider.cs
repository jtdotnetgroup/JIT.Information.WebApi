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
            //context.CreatePermission(ProductionPlanPermissionsNames.TaskPlan_Get, L("Get"));
            //context.CreatePermission(ProductionPlanPermissionsNames.TaskPlan_Create, L("Create"));
            //context.CreatePermission(ProductionPlanPermissionsNames.TaskPlan_Update,L("Update"));
            //context.CreatePermission(ProductionPlanPermissionsNames.TaskPlan_Delete, L("Delete"));

            PermissionSeter.SetPermission<ProductionPlanPermissionsNames>(context);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, "InformationSystem");
        }
    }
}