using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICQualityRpt : Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        [Key]
        public string FID { get; set; }
        public int FItemID { get; set; }
        public decimal? FAuxQty { get; set; }
        public string FRemark { get; set; }
        public string FNote { get; set; }
        /// <summary>
        /// 外键id
        /// </summary>

        public string ICMOInspectBillID { get; set; }
    }
}