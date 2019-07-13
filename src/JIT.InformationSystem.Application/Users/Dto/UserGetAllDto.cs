using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JIT.InformationSystem.CommonClass;

namespace JIT.InformationSystem.Users.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class UserGetAllDto: JITPagedResultRequestDto
    { 
        [DisplayName("用户名")]
        public string UserName { get; set; } 
        [DisplayName("姓名")]
        public string Surname { get; set; }
        [DisplayName("邮箱")]
        public string EmailAddress { get; set; }
        [DisplayName("注册日期")]
        public DateTime CreationTime { get; set; }
    }
}
