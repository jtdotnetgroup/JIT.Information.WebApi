using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JIT.DIME2Barcode.Entities
{
    public class VW_YSQty
    {
        public string FBillNo { get; set; }
        public string FBiller { get; set; }
        public string FName { get; set; }
        public DateTime FDate { get; set; }
        public string FBillNo2 { get; set; }
        public string BatchNum { get; set; }
        public int FYSQty { get; set; }
        public string FInspector { get; set; }
        public DateTime FInspectTime { get; set; } 
    }
}
