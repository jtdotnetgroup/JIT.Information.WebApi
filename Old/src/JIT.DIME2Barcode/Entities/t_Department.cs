using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class t_Department
    {
        [Key]
        public int FItemID { get; set; }
        public string FBrNO { get; set; }
        public int? FManager { get; set; }
        public string FPhone { get; set; }
        public string FFax { get; set; }
        public string FNote { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public int? FParentID { get; set; }
        public int FDProperty { get; set; }
        public int? FDStock { get; set; }
        public short FDeleted { get; set; }
        public string FShortNumber { get; set; }
        public int FAcctID { get; set; }
        public int FCostAccountType { get; set; }
        public byte[] FModifyTime { get; set; }
        public int FCalID { get; set; }
        public int? FPlanArea { get; set; }
        public int FOtherARAcctID { get; set; }
        public int FOtherAPAcctID { get; set; }
        public int FPreARAcctID { get; set; }
        public int FPreAPAcctID { get; set; }
        public bool? FIsCreditMgr { get; set; }
    }
}