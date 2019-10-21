using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using CommonTools;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos
{
    public class TB_BadItemRelationGetAllInput: JITPagedResultRequestDto
    {
        [Required]
        public int FOperID { get; set; }
        [DisplayName("不良编码")]
        public string FNumber { get; set; }
        [DisplayName("不良名称")]
        public string FName { get; set; }
        [DisplayName("产品")]
        public string FItemName { get; set; }//产品
        [DisplayName("工序名称")]
        public string FOperName { get; set; }//工序名称
    }
}
