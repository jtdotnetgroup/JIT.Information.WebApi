using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using JIT.InformationSystem.Authorization.Users;

namespace JIT.DIME2Barcode.SystemSetting.Employee.Dtos
{
    public class CreateEmployeeUserDto:EntityDto<long>
    {
       public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        //public bool IsActive { get; set; }
       // public string NormalizedUserName { get; set; }
       // public string NormalizedEmailAddress { get; set; }
       // public bool booIsEmailConfirmed { get; set; }
        //public bool IsTwoFactorEnabled { get; set; }

        //public bool  IsPhoneNumberConfirmed { get; set; }
       // public int AccessFailedCount { get; set; }

       // public bool IsLockoutEnabled { get; set; }
      // public DateTime CreationTime { get; set; }
      // public bool IsDeleted { get; set; }

       //public string[] RoleNames { get; set; }
       // public void Normalize()
       //{
       //    if (RoleNames == null)
       //    {
       //        RoleNames = new string[0];
       //    }
       // }
    }
}
