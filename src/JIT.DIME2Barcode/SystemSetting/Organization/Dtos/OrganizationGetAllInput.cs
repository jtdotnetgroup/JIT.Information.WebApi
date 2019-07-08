using System;
using System.ComponentModel;
using System.Data;
using Abp.Application.Services.Dto;
using CommonTools;
using JIT.DIME2Barcode.Model;

namespace JIT.DIME2Barcode.SystemSetting.Organization.Dtos
{
    public class OrganizationGetAllInput:JITPagedResultRequestDto
    {
        //组织类型，用于过滤公司、部门
        public Nullable<int> OrganizationType { get; set; }

        [DisplayName("是否车间")]
        public bool isWorkCenter { get; set; }

        public double dd { get; set; }
        public DateTime dt { get; set; }
        public long lg { get; set; }
        public string sg { get; set; }
        public bool? bb { get; set; }
        public decimal? Decimal { get; set; }

        public PublicEnum.FWorkingState ddwer { get; set; }

    }
}