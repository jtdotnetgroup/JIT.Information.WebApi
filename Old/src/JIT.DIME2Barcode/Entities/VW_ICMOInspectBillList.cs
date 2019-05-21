using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class VW_ICMOInspectBillList:Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        [Key]
        public string 派工单号 { get; set; }
        public string 产品名称 { get; set; }
        public Nullable<int> 工序 { get; set; }
        public Nullable<decimal> 汇报数 { get; set; }
        public Nullable<decimal> 合格数 { get; set; }
        public Nullable<decimal> 不合格数 { get; set; }
    }
}