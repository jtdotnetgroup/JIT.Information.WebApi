using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.Test.Dtos
{
    /// <summary>
    /// 条件
    /// </summary>
    public class UserRoleInput : PagedResultRequestDto
    {
        public int RoleId { get; set; }
        public int type { get; set; }
        public string UserName { get; set; }
    }
}
