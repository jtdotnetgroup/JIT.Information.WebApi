using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos
{
    public class TB_BadItemRelationGetAllInput: PagedResultRequestDto
    {
        [Required]
        public int FOperID { get; set; }
    }
}
