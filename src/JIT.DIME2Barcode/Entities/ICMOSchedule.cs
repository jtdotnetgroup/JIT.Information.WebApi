using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICMOSchedule:Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }

        [Key]
        public string FID { get; set; }
        public string FBillNo { get; set; }
        public int FTranType { get; set; }
        public int FMOInterID { get; set; }
        public string FMOBillNo { get; set; }
        public int FStatus { get; set; }
        public bool? FCancellation { get; set; }
        public bool? FClosed { get; set; }
        public decimal? FSrcAuxQty { get; set; }
        public decimal? FPlanAuxQty { get; set; }
        public decimal? FFinishAuxQty { get; set; }
        public decimal? FPassAuxQty { get; set; }
        public decimal? FFailAuxQty { get; set; }
        public DateTime? FPlanBeginDate { get; set; }
        public DateTime? FPlanFinishDate { get; set; }
        public DateTime? FFinishDate { get; set; }
        public string FBiller { get; set; }
        public DateTime? FBillTime { get; set; }
        public string FChecker { get; set; }
        public DateTime? FCheckTime { get; set; }
        public string FCloser { get; set; }
        public DateTime? FCloseTime { get; set; }
        public string FNote { get; set; }

        public List<ICMODaily> Dailies { get; set; }
    }
}