﻿using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace JIT.InformationSystem.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }
        
        public bool RememberClient { get; set; }
    }

    public class AuthenticateByIDCardModel
    {
        [Required]
        public string IDCardNumber { get; set; }
    }
}
