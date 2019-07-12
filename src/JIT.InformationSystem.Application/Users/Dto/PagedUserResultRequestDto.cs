using Abp.Application.Services.Dto;
using System;
using JIT.InformationSystem.CommonClass;

namespace JIT.InformationSystem.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : JITPagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        
    }

  

}
