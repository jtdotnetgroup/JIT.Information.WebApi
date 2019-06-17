using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.VW_RoleUserAll.Dtos
{

    /// <summary>
    ///  接收角色和数据表格的数据
    /// </summary>
    public class VW_RoleUserAllCreateDto:EntityDto<int>
    {
        public List<UserList> UserListsId { get; set; }
    }
    /// <summary>
    /// 用户的实体用于接收 userid 是否选中
    /// </summary>
    public class UserList
    {
        public int UserID { get; set; } //用户ID

        public bool UserBool { get; set; }//表格的复选框选中或者不选中
    }
}
