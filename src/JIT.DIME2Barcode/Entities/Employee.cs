using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JIT.DIME2Barcode.Entities
{
   public partial class Employee:Entity<int>
    {

       [StringLength(100)]
       public string FMpno { get; set; }
       [StringLength(10)]
        public string FName { get; set; }
        public int? FSex { get; set; }
        public int? FDepartment { get; set; } //所属部门  子ID
        public int? FWorkingState { get; set; }//在职状态
        public int? FSystemUser { get; set; }//系统用户
        public int? FParentId { get; set; }//上级主管  
        
        public string FPhone { get; set; }
        public   DateTime? FHiredate { get; set; }
        public string FEmailAddress { get; set; }
        public int? FERPUser { get; set; }
        public int? FERPOfficeClerk { get; set; }
        public int FTenantId { get; set; }
        public int FOrganizationUnitId { get; set; }//组织单元父ID

        public long FUserId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("FDepartment")]
        public OrganizationUnit Department { get; set; }
    }
}
