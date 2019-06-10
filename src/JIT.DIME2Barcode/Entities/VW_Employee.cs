using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class VW_Employee:Entity<int>
    {    
        public string FMpno { get; set; }
        public string FName { get; set; }
        public int FSex { get; set; }
        public int FDepartment { get; set; } //所属部门
        public int FWorkingState { get; set; }//在职状态
        public int FSystemUser { get; set; }//系统用户
        public int FParentId { get; set; }
        public string FPhone { get; set; }
        public DateTime? FHiredate { get; set; }
        public string FEmailAddress { get; set; }
        public int FERPUser { get; set; }
        public int FERPOfficeClerk { get; set; }
        public int FTenantId { get; set; }
        public int FOrganizationUnitId { get; set; }
        public long FUserId { get; set; }
        public bool IsDeleted { get; set; }
        public string fatherName { get; set; }//公司名称
        public string zsonName { get; set; }//部门名称
        public string rolename { get; set; }//角色名称
        public string remark { get; set; }
        public string UserName { get; set; }//用户账号
        public string Password { get; set; }//密码 用户绑定显示
    }
}
