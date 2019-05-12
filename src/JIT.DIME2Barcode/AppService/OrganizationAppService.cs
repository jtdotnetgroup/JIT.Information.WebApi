using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Organizations;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;

namespace JIT.DIME2Barcode.SystemSetting.Organization
{
    public class OrganizationAppService
        : AsyncCrudAppService<OrganizationUnit, OrganizationDto, long, OrganizationGetAllInput, OrganizationCreateInput, OrganizationDto, OrganizationDto, OrganizationDeleteInput>
    {
        public OrganizationUnitManager OrganizationUnitManager { get; set; }

        public OrganizationAppService(IRepository<OrganizationUnit, long> repository) : base(repository)
        {
        }



    }
}