using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class T_PrintTemplate
    { 
        [Key]
        public string FInterID { get; set; }
        public int? FUserID { get; set; }
        public int FTranType { get; set; }
        public string FType { get; set; }
        public int FItemID { get; set; }
        public string FFileDir { get; set; }
        public byte[] FFile { get; set; }
        public string FNote { get; set; }
    }
}