using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.Organization.Dtos
{
    public class OrganizationDeleteInput:EntityDto<long>
    {
        [Required]
        [DisplayName("组织代码")]
        public string Code { get; set; }
    }
}