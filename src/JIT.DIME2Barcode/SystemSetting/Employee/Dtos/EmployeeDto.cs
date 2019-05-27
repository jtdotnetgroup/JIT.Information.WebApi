using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.SystemSetting.Employee.Dtos
{
    public class EmployeeDto:EntityDto<int>
    {
        public string FMpno { get; set; }
        public string FName { get; set; }
        public int FSex { get; set; }
        public int FWorkingState { get; set; }
        public int FSystemUser { get; set; }
        public int FParentId { get; set; }
        public string FPhone { get; set; }
        public DateTime FHiredate { get; set; }

        public OrganizationUnit Department { get; set; }


    }
}
