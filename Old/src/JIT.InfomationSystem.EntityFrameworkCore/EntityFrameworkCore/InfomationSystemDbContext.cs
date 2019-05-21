using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using JIT.DIME2Barcode.Entities;
using JIT.InfomationSystem.Authorization.Roles;
using JIT.InfomationSystem.Authorization.Users;
using JIT.InfomationSystem.MultiTenancy;

namespace JIT.InfomationSystem.EntityFrameworkCore
{
    public class InfomationSystemDbContext : AbpZeroDbContext<Tenant, Role, User, InfomationSystemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public InfomationSystemDbContext(DbContextOptions<InfomationSystemDbContext> options)
            : base(options)
        {
        }

        
       
    }
}
