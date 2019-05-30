using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.Permissions
{
    public class ProductionPlanPermissionsNames:PermissionBase
    {
        private const string TaskPlan = "TaskPlan";
        private const string TaskDispatch = "TaskDispatch";

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
    }
}