using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class t_MeasureUnit
    {
        [Key]
        public int FMeasureUnitID { get; set; }
        public int FUnitGroupID { get; set; }
        public string FNumber { get; set; }
        public string FAuxClass { get; set; }
        public string FName { get; set; }
        public int? FConversation { get; set; }
        public decimal FCoefficient { get; set; }
        public int FPrecision { get; set; }
        public string FBrNo { get; set; }
        public int FItemID { get; set; }
        public int? FParentID { get; set; }
        public short FDeleted { get; set; }
        public string FShortNumber { get; set; }
        public string FOperDate { get; set; }
        public decimal FScale { get; set; }
        public short FStandard { get; set; }
        public short FControl { get; set; }
        public byte[] FModifyTime { get; set; }
        public int FSystemType { get; set; }
        public Guid UUID { get; set; }
        public string FNameEN { get; set; }
        public string FNameEnPlu { get; set; }
    }
}