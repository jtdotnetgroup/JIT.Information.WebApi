using System;
using System.Collections.Generic;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;

namespace JIT.DIME2Barcode.TaskAssignment.ICMOScheduling.Dtos
{
    public class ICMOSchedulingDto
    {
        public string FID { get; set; }
        public DateTime? FBillTime {get; set; }
        public string FBillNo { get; set; }
        public string FBiller { get; set; }
        public decimal FPlanAuxQty { get; set; }

        public List<Entities.ICMODaily> Dailies { get; set; }
    }
}