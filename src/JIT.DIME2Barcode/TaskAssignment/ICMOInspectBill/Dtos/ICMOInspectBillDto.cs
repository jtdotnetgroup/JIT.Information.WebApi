using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.DIME2Barcode.TaskAssignment.ICMOInspectBill.Dtos
{
    /// <summary>
    /// 质检汇报明细
    /// </summary>

    public class ICMODispBillDetailedInput
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int FItemID { get; set; }
        /// <summary>
        /// 检验单号（批号）
        /// </summary>

        public string FBillNo { get; set; }
        /// <summary>
        /// 工序内码
        /// </summary>
        public int FOperID { get; set; }
    }
    /// <summary>
    /// 明细列
    /// </summary>

    public class ICMODispBillDetailed
    {
        public Entities.ICMOInspectBill IcmoInspectBill { get; set; }
        public List<Entities.ICQualityRpt> IcQualityRptsList { get; set; }

        //{
        //    /// <summary>
        //    /// 列
        //    /// </summary>
        //    public string Col { get; set; }
        //    /// <summary>
        //    /// 不良项目代码
        //    /// </summary>
        //    public string FNumber { get; set; }
        //    /// <summary>
        //    /// 项目名称
        //    /// </summary>

        //    public string ColName { get; set; }
        //    /// <summary>
        //    /// 数量
        //    /// </summary>
        //    public int ColNum { get; set; }
    } 
}
