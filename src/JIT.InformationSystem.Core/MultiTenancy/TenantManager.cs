using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.Editions;

namespace JIT.InformationSystem.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
