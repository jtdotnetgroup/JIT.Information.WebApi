using System;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.VW_ICMOInspectBillList.Dtos
{
    public class VW_ICMOInspectBillListDto:EntityDto
    {
        public string 派工单号 { get; set; }
        public string 产品名称 { get; set; }
        public Nullable<int> 工序 { get; set; }
        public Nullable<decimal> 汇报数 { get; set; }
        public Nullable<decimal> 合格数 { get; set; }
        public Nullable<decimal> 不合格数 { get; set; }
    }
}