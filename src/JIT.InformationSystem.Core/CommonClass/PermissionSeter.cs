using System.Linq;
using System.Reflection;
using Abp.Authorization;

namespace JIT.InformationSystem.CommonClass
{
    public class PermissionSeter
    {
        public static void SetPermission<TPermissionNames>(IPermissionDefinitionContext context) where TPermissionNames :PermissionBase
        {
            var t = typeof(TPermissionNames);

            var fields = t.GetRuntimeFields().Where(p=>p.IsPublic);

            foreach (var field in fields)
            {
                var permissionName = field.GetRawConstantValue().ToString();
                context.CreatePermission(permissionName);
            }
        }
    }
}