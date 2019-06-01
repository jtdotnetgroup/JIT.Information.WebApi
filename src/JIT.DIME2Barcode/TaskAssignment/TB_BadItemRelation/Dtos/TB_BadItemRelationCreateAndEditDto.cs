using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos
{
   public class TB_BadItemRelationCreateAndEditDto:EntityDto<int>
    {
   
        public int FID { get; set; }
        public int  FItemID { get; set; }
        public int FOperID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        [DefaultValue(false)]//设置默认值
        public bool FDeleted { get; set; }
        public string FRemark { get; set; }


    }
}
