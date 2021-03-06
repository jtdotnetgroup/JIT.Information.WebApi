﻿using Abp.EntityFrameworkCore;
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
        public virtual DbSet<ICQualityRpt> ICQualityRpt { get; set; }
        public virtual DbSet<ICBOM> ICBOM { get; set; }
        public virtual DbSet<SEOrder> SEOrder { get; set; }
        public virtual DbSet<t_Department> t_Department { get; set; }
        public virtual DbSet<t_MeasureUnit> t_MeasureUnit { get; set; }
        public virtual DbSet<T_PrintTemplate> T_PrintTemplate { get; set; }
        public virtual DbSet<TB_BadItemRelation> TB_BadItemRelation { get; set; }
    }
}