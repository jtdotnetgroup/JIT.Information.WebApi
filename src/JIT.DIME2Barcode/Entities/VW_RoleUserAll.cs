using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
   public class VW_RoleUserAll:Entity<int>
    {
        //public int Id { get; set; }
        public override int Id { get; set; }
        public  int? UserRoleID { get; set; }//角色用户表主键
        public  string UserName { get; set; }     
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int? RoleId { get; set; }
        public int? TenantId { get; set; }
    }
}
