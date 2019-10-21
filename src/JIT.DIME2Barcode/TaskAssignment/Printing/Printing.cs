using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.DIME2Barcode.TaskAssignment.Printing
{
    /// <summary>
    /// 质检单打印模板数据
    /// </summary>
    public class Printing
    {

        /// <summary>
        /// 品番代码
        /// </summary>
        public string ItemNum { get; set; }
        /// <summary>
        /// 品番名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string LotNum { get; set; }
        /// <summary>
        /// 合格数，重量
        /// </summary>
        public decimal? PrintQty { get; set; }
        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// 包装数量
        /// </summary>
        public decimal PackQty { get; set; }

        public string Biller { get; set; }
    }
}