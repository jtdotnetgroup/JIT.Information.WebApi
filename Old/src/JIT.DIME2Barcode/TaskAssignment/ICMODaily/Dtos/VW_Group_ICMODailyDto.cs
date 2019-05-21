using System;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class VW_Group_ICMODailyDto:EntityDto<string>
    {
        public string FID { get; set; }
        public DateTime? 日期 { get; set; }
        public int? 机台 { get; set; }
        public int? 班组 { get; set; }
        public string 操作员 { get; set; }
        public decimal 计划数量 { get; set; }
        public decimal 派工数量 { get; set; }
        public decimal 完成数量 { get; set; }
        public decimal 合格数量 { get; set; }
        public string FMOBillNo { get; set; }
        public int? FMOInterID { get; set; }
    }
}