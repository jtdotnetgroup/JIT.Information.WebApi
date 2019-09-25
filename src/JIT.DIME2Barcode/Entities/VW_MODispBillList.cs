using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class VW_MODispBillList : Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        
        public string 工作中心 { get; set; }
        public string 设备 { get; set; }
        public System.DateTime 生产日期 { get; set; }
        public string 班次 { get; set; }
        public string 生产任务 { get; set; }
        public string 派工单号 { get; set; }
        public long 操作者 { get; set; }
        public string 产品代码 { get; set; }
        public string 产品名称 { get; set; }
        public string 规格型号 { get; set; }
        public string 工序 { get; set; }
        public Nullable<decimal> 计划数量 { get; set; }
        public Nullable<decimal> 派工数量 { get; set; }
        public Nullable<decimal> 汇报数量 { get; set; }
        public Nullable<decimal> 合格数量 { get; set; }
        public Nullable<decimal> 不合格数量 { get; set; }
        public int 打印次数 { get; set; }
        [Key]
        public string FID { get; set; }
        public int FStatus { get; set; }
        public Nullable<bool> FClosed { get; set; }
        public int FItemID { get; set; } 
        public Nullable<int> FWorkcenterID { get; set; }
        public string FsrcID { get; set; }
        public int FOperID { get; set; }

        public DateTime 计划开工日期 { get; set; }
        public DateTime 计划完工日期 { get; set; }

        public double F_102 { get; set; }
        public int FWorker { get; set; }
        public DateTime? IsYC { get; set; }
        //public string WorkName { get; set; }
    }
}
