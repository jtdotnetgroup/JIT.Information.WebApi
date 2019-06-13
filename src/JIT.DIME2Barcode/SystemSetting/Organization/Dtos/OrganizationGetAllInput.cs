using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.Organization.Dtos
{
    public class OrganizationGetAllInput:PagedResultRequestDto
    {
        //组织类型，用于过滤公司、部门
        public int? OrganizationType { get; set; }

        public bool isWorkCenter { get; set; }
    }
}