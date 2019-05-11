using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class VW_ICMODailyDto:EntityDto<string>
    {
        public System.DateTime 日期 { get; set; }
        [Required]
        public string 计划单号 { get; set; }
        public string 任务单号 { get; set; }
        public string 车间 { get; set; }
        public string 产品编码 { get; set; }
        public string 产品名称 { get; set; }
        public string 规格型号 { get; set; }
        public Nullable<decimal> 计划数量 { get; set; }
        public Nullable<System.DateTime> 计划开工日期 { get; set; }
        public Nullable<System.DateTime> 计划完工日期 { get; set; }
        public decimal 完成数量 { get; set; }
    }
}