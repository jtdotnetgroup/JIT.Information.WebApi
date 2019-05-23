using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.Organization.Dtos
{
    public class OrganizationDtoTest: EntityDto<long>
    {
        public virtual int? TenantId { get; set; }

 
        public virtual long? ParentId { get; set; }

    
        public virtual string Code { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string title { get; set; }
        public virtual string key { get; set; }

        public List<OrganizationDtoTest> children { get; set; }


    }
}
