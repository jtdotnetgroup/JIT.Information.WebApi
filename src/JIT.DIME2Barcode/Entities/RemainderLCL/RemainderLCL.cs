using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 余数拼箱
    /// </summary>
    public class RemainderLCL : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get { return LCLId; }
            set { Id = LCLId; }
        }
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public string LCLId { get; set; }
        /// <summary>
        /// 拼单号
        /// </summary>
        public string LCLCode { get; set; }
        /// <summary>
        /// 拼单日期
        /// </summary>
        public DateTime LCLTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
