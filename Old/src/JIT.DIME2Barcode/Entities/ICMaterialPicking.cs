using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICMaterialPicking
    {
        [Key]
        public string FID { get; set; }
        public string FSrcID { get; set; }
        public int FEntryID { get; set; }
        public int? FItemID { get; set; }
        public int? FUnitID { get; set; }
        public string FBatchNo { get; set; }
        public decimal? FAuxQty { get; set; }
        public string FBiller { get; set; }
        public DateTime? FDate { get; set; }
        public string FNote { get; set; }
    }
}