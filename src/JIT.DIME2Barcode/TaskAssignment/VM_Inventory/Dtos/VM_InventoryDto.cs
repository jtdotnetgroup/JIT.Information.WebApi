using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.VM_Inventory.Dtos
{
    public class VM_InventoryDto  
    { 
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
        public int FItemID { get; set; }
        public int FAuxPropID { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class VM_InventoryGetAllInput : PagedAndSortedResultRequestDto
    { 
    }
}
