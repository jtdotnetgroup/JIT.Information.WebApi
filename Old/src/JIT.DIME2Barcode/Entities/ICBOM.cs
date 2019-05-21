using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICBOM:Entity
    {
        [NotMapped]
        public override int Id { get; set; }
        public string FBrNo { get; set; }
        [Key]
        public int FInterID { get; set; }
        public string FBOMNumber { get; set; }
        public short FImpMode { get; set; }
        public int? FUseStatus { get; set; }
        public string FVersion { get; set; }
        public int? FParentID { get; set; }
        public int FItemID { get; set; }
        public decimal FQty { get; set; }
        public decimal? FYield { get; set; }
        public int? FCheckID { get; set; }
        public DateTime? FCheckDate { get; set; }
        public int? FOperatorID { get; set; }
        public DateTime? FEnterTime { get; set; }
        public short FStatus { get; set; }
        public bool? FCancellation { get; set; }
        public int FTranType { get; set; }
        public int FRoutingID { get; set; }
        public int FBomType { get; set; }
        public int FCustID { get; set; }
        public int FCustItemID { get; set; }
        public int FAccessories { get; set; }
        public string FNote { get; set; }
        public int FUnitID { get; set; }
        public decimal FAUXQTY { get; set; }
        public int? FCheckerID { get; set; }
        public DateTime? FAudDate { get; set; }
        public int FEcnInterID { get; set; }
        public bool? FBeenChecked { get; set; }
        public short FForbid { get; set; }
        public int FAuxPropID { get; set; }
        public DateTime? FPDMImportDate { get; set; }
        public short FBOMSkip { get; set; }
        public int? FClassTypeID { get; set; }
        public int? FUserID { get; set; }
        public DateTime? FUseDate { get; set; }
    }
}