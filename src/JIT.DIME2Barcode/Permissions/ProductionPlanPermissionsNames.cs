using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.Permissions
{
    public class ProductionPlanPermissionsNames:PermissionBase
    {
        private const string TaskPlan = "TaskPlan";
        private const string TaskDispatch = "TaskDispatch";
        private const string TouchPad = "TouchPad";

        //任务排产
        public const string TaskPlan_Get = TaskPlan + Get;
        public const string TaskPlan_Update = TaskPlan + Update;
        public const string TaskPlan_Delete = TaskPlan + Delete;
        public const string TaskPlan_Create = TaskPlan + Create;
        public const string TaskPlan_Import = TaskPlan + Import;

        //任务派工
        public const string TaskDispatch_Get = TaskDispatch + Get;
        public const string TaskDispatch_Update = TaskDispatch + Update;
        public const string TaskDispatch_Delete = TaskDispatch + Delete;
        public const string TaskDispatch_Create = TaskDispatch + Create;

        //触屏端权限
        //派工任务
        public const string TouchPadDispatchedWork = "TouchPad.DispatchedWork";
        //质检
        public const string TouchPadIPQC = "TouchPad.IPQC";
        //库存
        public const string TouchPadStock = "TouchPad.Stock";
        //完工报检
        //public const string TouchPadInspection = "TouchPad.Inspection";
        ////完工汇报
        //public const string TouchPadReport = "TouchPad.Report";
        ////我的报表
        //public const string TouchPadJournaling = "TouchPad.Journaling";
        ////生产领料
        //public const string TouchPadProduction = "TouchPad.Production";
        //条码打印
        public const string TouchPadBarCode = "TouchPad.BarCode";
    }
}