using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos
{
    public class TB_BadItemRelationDto:EntityDto<int>
    {
      
        public int FID { get; set; }
        public int? FItemID { get; set; }
        public int FOperID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public bool? FDeleted { get; set; }
        public string FRemark { get; set; }

        public string FItemName { get; set; }//产品
        public string FOperName { get; set; }//工序名称


    }
}
