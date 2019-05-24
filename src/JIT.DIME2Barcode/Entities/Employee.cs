using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
   public partial class Employee:Entity<int>
    {

       [StringLength(100)]
       public string FMpno { get; set; }
       [StringLength(10)]
        public string FName { get; set; }
        public int? FSex { get; set; }
        public int? FDepartment { get; set; } //所属部门
        public int? FWorkingState { get; set; }//在职状态
        public int? FSystemUser { get; set; }//系统用户
        public int? FParentId { get; set; }
        
        public string FPhone { get; set; }
        public   DateTime? FHiredate { get; set; }
        public string FEmailAddress { get; set; }
        public int? FERPUser { get; set; }
        public int? FERPOfficeClerk { get; set; }
        public int FTenantId { get; set; }
        public int FOrganizationUnitId { get; set; }

        public long FUserId { get; set; }
        public bool IsDeleted { get; set; }

    }
}
