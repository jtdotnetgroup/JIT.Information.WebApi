using Abp.MultiTenancy;
using JIT.InfomationSystem.Authorization.Users;

namespace JIT.InfomationSystem.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
