using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICMOInspectBill
    {
        [Key]
        public string FID { get; set; }
        public int? FMOInterID { get; set; }
        public string FBillNo { get; set; }
        public int FTranType { get; set; }
        public int FStatus { get; set; }
        public int? FOperID { get; set; }
        public int? FWorkcenterID { get; set; }
        public int? FMachineID { get; set; }
        public decimal? FAuxQty { get; set; }
        public decimal? FCheckAuxQty { get; set; }
        public decimal? FPassAuxQty { get; set; }
        public decimal? FFailAuxQty { get; set; }
        public decimal? FFailAuxQtyP { get; set; }
        public decimal? FFailAuxQtyM { get; set; }
        public decimal? FPassAuxQtyP { get; set; }
        public decimal? FPassAuxQtyM { get; set; }
        public string FNote { get; set; }
        public string FWorker { get; set; }
        public string FInspector { get; set; }
        public DateTime? FInspectTime { get; set; }
        public string FBiller { get; set; }
        public DateTime? FBillTime { get; set; }
        public string FChecker { get; set; }
        public DateTime? FCheckTime { get; set; }
    }
}