using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using JIT.InformationSystem.Authorization.Roles;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.MultiTenancy;

namespace JIT.InformationSystem.EntityFrameworkCore
{
    public class InformationSystemDbContext : AbpZeroDbContext<Tenant, Role, User, InformationSystemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public InformationSystemDbContext(DbContextOptions<InformationSystemDbContext> options)
            : base(options)
        {
        }

    }
}
