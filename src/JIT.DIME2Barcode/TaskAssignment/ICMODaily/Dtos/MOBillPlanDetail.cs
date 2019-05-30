using System;
using System.Collections.Generic;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class MOBillPlanDetail
    {
        public int FMOInterID { get; set; }
        public decimal? TotalPlan { get; set; }
        public decimal? TotalCommit { get; set; }

        public List<MOBillPlanDay> Details { get; set; }
    }

    public class MOBillPlanDay
    {
        public DateTime FDate { get; set; }
        public decimal? DayPlan { get; set; }
        public decimal? DayCommit { get; set; }
    }
}