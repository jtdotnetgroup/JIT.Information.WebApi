using Abp.EntityFrameworkCore;
using JIT.DIME2Barcode.SystemSetting.LogManager;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
using JIT.InformationSystem;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.Entities
{
    public class ProductionPlanMySqlDbContext:AbpDbContext
    {
        public ProductionPlanMySqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ICMODaily> ICMODaily { get; set; }
        public virtual DbSet<BillStatus> BillStatus { get; set; }
        public virtual DbSet<ICException> ICException { get; set; }
        public virtual DbSet<ICMaterialPicking> ICMaterialPicking { get; set; }
        public virtual DbSet<ICMO> ICMO { get; set; }
        public virtual DbSet<ICMODispBill> ICMODispBill { get; set; }
        public virtual DbSet<ICMOInspectBill> ICMOInspectBill { get; set; }
        public virtual DbSet<ICMOSchedule> ICMOSchedule { get; set; }
        public virtual DbSet<ICQualityRpt> ICMOQualityRpt { get; set; }
        public virtual DbSet<ICBOM> ICBOM { get; set; }
        public virtual DbSet<SEOrder> SEOrder { get; set; }
        public virtual DbSet<t_Department> t_Department { get; set; }
        public virtual DbSet<t_MeasureUnit> t_MeasureUnit { get; set; }
        public virtual DbSet<T_PrintTemplate> T_PrintTemplate { get; set; }
        public virtual DbSet<TB_BadItemRelation> TB_BadItemRelation { get; set; }
        public virtual DbSet<Equipment> t_equipment { get; set; }
        public virtual DbSet<t_OrganizationUnit> t_Organizationunit { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<VW_DispatchBill_List> VW_DispatchBill_List { get; set; }
        public virtual DbSet<EqiupmentShift> T_EquimentShift { get; set; }
        
        public virtual DbSet<SyncRecord> t_SyncRecord { get; set; }
        //public virtual DbSet<ICItem_Sync> t_ICItem { get; set; }
        public virtual DbSet<t_SubMessage> t_SubMessage { get; set; }
        public virtual DbSet<t_SubMesType> t_SubMesType { get; set; }

        public virtual DbSet<t_ICItem> t_ICItem { get; set; }

        public virtual DbSet<Abpauditlogs> Abpauditlogs { get; set; }
        //public virtual DbSet<t_ICItem> t_ICItem { get; set; }
        public virtual DbSet<Sys_Task> Sys_Task { get; set; }
        public virtual DbSet<Sys_TaskRecord> Sys_TaskRecord { get; set; }
        public virtual DbSet<Sys_BasicInfo> Sys_BasicInfo { get; set; }
        public virtual DbSet<RemainderLCL> RemainderLCL { get; set; }
        public virtual DbSet<RemainderLCLMx> RemainderLCLMx { get; set; }
        public virtual DbSet<VM_ICMOInspectBillED> VM_ICMOInspectBillED { get; set; }
        #region 视图


        public virtual DbSet<VW_ICMODaily> VW_ICMODaily { get; set; }
        public virtual DbSet<VW_MODispBillList> VW_MODispBillList { get; set; }
        public virtual DbSet<VW_ICMODaily_Group_By_Day> VW_ICMODaily_Group_By_Day { get; set; }
        public virtual DbSet<VW_Employee> VW_Employee { get; set; }
        public virtual DbSet<VW_Group_ICMODaily> VW_Group_ICMODaily { get; set; }
        public virtual DbSet<vw_PrintLabel> vw_PrintLabel { get; set; }

        public virtual DbSet<VW_RoleUserAll> VW_RoleUserAll { get; set; } //角色成员的视图

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>().HasIndex(p => p.FNumber).IsUnique();

            //员工表的 员工编号唯一约束
            modelBuilder.Entity<Employee>().HasIndex(p => p.FMpno).IsUnique();
        }
    }
}