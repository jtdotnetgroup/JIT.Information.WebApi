using Abp.MultiTenancy;
using JIT.InformationSystem.Authorization.Users;

namespace JIT.InformationSystem.MultiTenancy
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
