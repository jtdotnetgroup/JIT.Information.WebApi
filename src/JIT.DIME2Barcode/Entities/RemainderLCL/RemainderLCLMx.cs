using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 余数拼箱明细表
    /// </summary>
    public class RemainderLCLMx : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get { return LCLMxId; }
            set { Id = LCLMxId; }
        } 
        /// <summary>
        /// 唯一ID,主键ID
        /// </summary>
        [Key]
        public string LCLMxId { get; set; }
        /// <summary>
        /// 主表Id
        /// </summary>
        public string RemainderLCLId { get; set; }
        /// <summary>
        /// 质检单Id
        /// </summary>
        public string ICMOInspectBillId { get; set; }
        /// <summary>
        /// 已拼数量
        /// </summary>
        public decimal SpelledQty { get; set; }
        /// <summary>
        /// 拼单日期
        /// </summary>
        public DateTime LCLMxTime { get; set; }

    }
}
