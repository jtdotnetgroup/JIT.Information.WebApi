using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.VW_RoleUserAll.Dtos
{
    public   class PagedUserResultRequestRoleStaicDto: PagedResultRequestDto
    {
        //用于进行角色条件的筛选
        public int RoleStaic { get; set; }

        public  int RoleId { get; set; }
    }
}
