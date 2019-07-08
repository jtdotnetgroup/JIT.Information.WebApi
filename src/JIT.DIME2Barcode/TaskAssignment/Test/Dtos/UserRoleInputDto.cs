using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;

namespace JIT.DIME2Barcode.TaskAssignment.Test.Dtos
{
    /// <summary>
    /// 条件
    /// </summary>
    public class UserRoleInput : JITPagedResultRequestDto
    {
        public int RoleId { get; set; }
        public int type { get; set; }
        public string UserName { get; set; }
    }
}
