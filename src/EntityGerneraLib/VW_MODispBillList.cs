//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityGerneraLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class VW_MODispBillList
    {
        public string 设备 { get; set; }
        public string 工作中心 { get; set; }
        public System.DateTime 生产日期 { get; set; }
        public Nullable<int> 班次 { get; set; }
        public string 生产任务 { get; set; }
        public string 派工单号 { get; set; }
        public string 操作者 { get; set; }
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
        public string FID { get; set; }
        public int FStatus { get; set; }
        public Nullable<bool> FClosed { get; set; }
        public Nullable<int> FItemID { get; set; }
        public Nullable<int> FWorkCenterID { get; set; }
    }
}