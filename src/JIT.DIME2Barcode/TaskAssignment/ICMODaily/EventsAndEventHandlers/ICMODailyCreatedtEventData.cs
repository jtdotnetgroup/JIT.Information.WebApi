using Abp.Events.Bus;
using Abp.Events.Bus.Entities;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.EventsAndEventHandlers
{
    /// <summary>
    /// 实体插入后的事件
    /// </summary>
    public class ICMODailyCreatedtEventData:EventData
    {
        public string FSrcID { get; set; }

        public string FMOBillNo { get; set; }

        public int FMOInterID { get; set; }

        public string FBiller { get; set; }

        public decimal? FPlanAuxQty { get; set; }

    }
}