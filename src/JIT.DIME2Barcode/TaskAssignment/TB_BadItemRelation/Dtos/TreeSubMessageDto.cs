using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos
{
   public class TreeSubMessageDto:EntityDto<int>
    {   
        
        public virtual string title { get; set; }
        public virtual string key { get; set; }       
    }
}
