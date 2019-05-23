using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.Employee.Dtos
{
    public class EmployeeEdit:EntityDto<int>
    {
        public string FMpno { get; set; }
        public string FName { get; set; }
        public int FSex { get; set; }
        public int FDepartment { get; set; }
        public int FWorkingState { get; set; }
        public int FSystemUser { get; set; }
        public int FParentId { get; set; }
        public string FPhone { get; set; }
        public DateTime FHiredate { get; set; }
        public string FEmailAddress { get; set; }
        public int FERPUser { get; set; }
        public int FERPOfficeClerk { get; set; }
        public int FTenantId { get; set; }
        public int FOrganizationUnitId { get; set; }
    }
}
