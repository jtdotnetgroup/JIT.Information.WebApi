using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class VW_ICMODaily:Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        public System.DateTime 日期 { get; set; }
        [Key]
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