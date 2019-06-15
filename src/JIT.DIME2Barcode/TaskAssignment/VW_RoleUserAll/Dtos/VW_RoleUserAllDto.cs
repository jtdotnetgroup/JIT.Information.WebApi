using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Application.Services.Dto;
using Castle.Components.DictionaryAdapter;

namespace JIT.DIME2Barcode.TaskAssignment.VW_RoleUserAll.Dtos
{
    public  class VW_RoleUserAllDto
    {
        public int Id { get; set; }
        public int? UserRoleID { get; set; }//角色用户表主键
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int? RoleId { get; set; }
        public int? TenantId { get; set; }
    }
}
