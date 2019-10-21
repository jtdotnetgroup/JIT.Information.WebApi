using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class GroupDailyListInput:JITPagedResultRequestDto
    {
        [DisplayName("日期")]
        public DateTime FDate { get; set; }
        [DisplayName("任务单号")]
        public string FMOBillNo { get; set; }
        [DisplayName("车间")]
        public string DisplayName { get; set; } 
        [DisplayName("产品编码")]
        public string FItemNumber { get; set; }
        [DisplayName("产品名称")]
        public string FItemName { get; set; }
        [DisplayName("产品规格")]
        public string FItemModel { get; set; }
        [DisplayName("计划数量")]
        public decimal TotalPlanAuxQty { get; set; }

        [DisplayName("班次")]
        public string Fshift { get; set; }
        
    }
}
