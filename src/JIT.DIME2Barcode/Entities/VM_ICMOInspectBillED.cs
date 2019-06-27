using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public class VM_ICMOInspectBillED : Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        [Key]
        public string FID { get; set; }

        public string FBillNo { get; set; }
        public string FBiller { get; set; }
        public DateTime FDate { get; set; }
        public string FBillNo2 { get; set; }
        public string BatchNum { get; set; }
        public decimal FYSQty { get; set; }
        public string FInspector { get; set; }
        public DateTime FInspectTime { get; set; }
        public string FName { get; set; }
        public int FStatus { get; set; }
        public string FItemName { get; set; }
        public double? F_102 { get; set; }
    }
}
