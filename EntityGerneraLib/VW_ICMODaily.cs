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
    
    public partial class VW_ICMODaily
    {
        
        public System.DateTime 日期 { get; set; }
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
