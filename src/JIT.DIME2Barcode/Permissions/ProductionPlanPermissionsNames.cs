using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.Permissions
{
    public class ProductionPlanPermissionsNames:PermissionBase
    {
        #region 管理端
        // 功能栏
        private const string TaskPlan = "TaskPlan";
        private const string TaskDispatch = "TaskDispatch";
        private const string TouchPad = "TouchPad";
        private const string BasicData = "BasicData";
        private const string EquipmentArchives = "EquipmentArchives";
        private const string BadProjects = "BadProjects";
        private const string UsersManagement = "UsersManagement";
        private const string RolesManagement = "RolesManagement";
        private const string JournalQuery = "JournalQuery";
        private const string ReportForm = "ReportForm";
        private const string WorkCenter = "WorkCenter";
        private const string BasicInfo = "BasicInfo";

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

        // 基础资料-组织
        public const string BasicData_OrganizeAdd = BasicData + ".OrganizeAdd";
        public const string BasicData_OrganizeGet = BasicData + ".OrganizeGet";
        public const string BasicData_OrganizeUpdate = BasicData + ".OrganizeUpdate";
        public const string BasicData_OrganizeDelete = BasicData + ".OrganizeDelete";

        // 基础资料-员工
        public const string BasicData_StaffAdd = BasicData + ".StaffAdd";
        public const string BasicData_StaffGet = BasicData + ".StaffGet";
        public const string BasicData_StaffUpdate = BasicData + ".StaffUpdate";
        public const string BasicData_StaffDelete = BasicData + ".StaffDelete";

        // 设备档案
        public const string EquipmentArchives_Get = EquipmentArchives + Get;
        public const string EquipmentArchives_Update = EquipmentArchives + Update;
        public const string EquipmentArchives_Delete = EquipmentArchives + Delete; 
        public const string EquipmentArchives_DiscontinueUse = EquipmentArchives + ".DiscontinueUse";
        public const string EquipmentArchives_Scrap = EquipmentArchives + ".Scrap";
        public const string EquipmentArchives_Repair = EquipmentArchives + ".Repair";
        public const string EquipmentArchives_ResourceImport = EquipmentArchives + ".ResourceImport";
        public const string EquipmentArchives_CapacityImport = EquipmentArchives + ".CapacityImport";
        public const string EquipmentArchives_FlightInformationMaintenance = EquipmentArchives + ".FlightInformationMaintenance";

        // 不良项目
        public const string BadProjects_Get = BadProjects + Get;
        public const string BadProjects_Update = BadProjects + Update;
        public const string BadProjects_Create = BadProjects + Create;
        public const string BadProjects_Import = BadProjects + Import;
        public const string BadProjects_Export = BadProjects + Export;
        public const string BadProjects_Delete = BadProjects + Delete;

        // 用户管理
        public const string UsersManagement_Get = UsersManagement + Get;
        public const string UsersManagement_Update = UsersManagement + Update;
        public const string UsersManagement_Create = UsersManagement + Create;
        public const string UsersManagement_Delete = UsersManagement + Delete;
        public const string UsersManagement_Refresh = UsersManagement + Refresh;

        // 角色管理
        public const string RolesManagement_Get = RolesManagement + Get;
        public const string RolesManagement_Update = RolesManagement + Update;
        public const string RolesManagement_Create = RolesManagement + Create;
        public const string RolesManagement_Delete = RolesManagement + Delete;
        public const string RolesManagement_MemberManagement = RolesManagement + ".MemberManagement";
        //public const string Roles_Refresh = Roles + Refresh;

        // 日志查询
        public const string JournalQuery_Get = JournalQuery + Get;

        // 报表管理
        public const string ReportForm_Get = ReportForm + Get;
        
        // 工作中心
        public const string WorkCenter_Get = WorkCenter + Get;

        // 基础信息
        public const string BasicInfo_Get = BasicInfo + Get;
        public const string BasicInfo_Update = BasicInfo + Update;
        public const string BasicInfo_Create = BasicInfo + Create;
        public const string BasicInfo_Delete = BasicInfo + Delete;
        #endregion

        #region 触屏端权限
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
        // 包装余数
        public const string TouchPadPackNum = "TouchPad.PackNum";
        #endregion
    }
}