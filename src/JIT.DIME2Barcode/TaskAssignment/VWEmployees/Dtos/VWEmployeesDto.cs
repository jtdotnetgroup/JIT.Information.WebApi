using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.TaskAssignment.VWEmployees
{
   public class VWEmployeesDto:EntityDto<int>
    {
        public string FMpno { get; set; }
        public string FName { get; set; }
        public int? FSex { get; set; }
        public int? FDepartment { get; set; } //所属部门
        public int? FWorkingState { get; set; }//在职状态
        public int? FSystemUser { get; set; }//系统用户
        public int? FParentId { get; set; }
        public string FPhone { get; set; }
        public DateTime? FHiredate { get; set; }
        public string FEmailAddress { get; set; }
        public int? FERPUser { get; set; }
        public int? FERPOfficeClerk { get; set; }
        public int FTenantId { get; set; }
        public int FOrganizationUnitId { get; set; }
        public long FUserId { get; set; }
        public bool IsDeleted { get; set; }
        public string fatherName { get; set; }
        public string zsonName { get; set; }

        public string rolename { get; set; }

        public string remark { get; set; }

        public string UserName { get; set; }//用户账号
        public string Password { get; set; }//密码 用户绑定显示
    }
   public class VWEmployeesGetAllInputDto : JITPagedResultRequestDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("员工编号")]
        public string FMpno { get; set; }
        [DisplayName("姓名")]
        public string FName { get; set; } 
    }

}
