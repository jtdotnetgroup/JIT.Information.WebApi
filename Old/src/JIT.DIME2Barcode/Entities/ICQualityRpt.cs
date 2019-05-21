using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICQualityRpt
    {
        [Key]
        public string FID { get; set; }
        public int FItemID { get; set; }
        public decimal? FAuxQty { get; set; }
        public string FRemark { get; set; }
        public string FNote { get; set; }
    }
}