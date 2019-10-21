using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public class VM_Inventory : Entity<int>
    {
        [NotMapped]
        public override int Id { get; set; }
       
        public string 仓库 { get; set; }
        public string 仓位 { get; set; }
        public string 物料编码 { get; set; }
        public string 物料名称 { get; set; }
        public string 规格型号 { get; set; }
        public string 单位 { get; set; }
        public string 辅助属性 { get; set; }
        public string 批号 { get; set; }
        public Decimal 库存数量 { get; set; }

        public int FStockID { get; set; }
        public int FSPID { get; set; }
        [Key]
        public int FItemID { get; set; }
        public int FAuxPropID { get; set; }
    }
}
