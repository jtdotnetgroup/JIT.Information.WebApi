using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace JIT.DIME2Barcode.Entities
{

    public partial class Dime2barcodeContext : AbpDbContext,ITransientDependency
    {
        public Dime2barcodeContext(DbContextOptions<Dime2barcodeContext> options)
            : base(options)
        {
        }

        public virtual Microsoft.EntityFrameworkCore.DbSet<VW_Group_ICMODaily> VW_Group_ICMODaily { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMODaily> ICMODaily { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<BillStatus> BillStatus { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICException> ICException { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMaterialPicking> ICMaterialPicking { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMO> ICMO { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMODispBill> ICMODispBill { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMOInspectBill> ICMOInspectBill { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICMOSchedule> ICMOSchedule { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICQualityRpt> ICQualityRpt { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<ICBOM> ICBOM { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<SEOrder> SEOrder { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<t_Department> t_Department { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<t_MeasureUnit> t_MeasureUnit { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<T_PrintTemplate> T_PrintTemplate { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<TB_BadItemRelation> TB_BadItemRelation { get; set; }
        //public virtual Microsoft.EntityFrameworkCore.DbSet<t_ICItem> t_ICItem { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<VW_MOBillList> VW_MOBillList { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<VW_ICMODaily> VW_ICMODaily { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<VW_ICMODispBill_By_Date> VW_ICMODispBill_By_Date { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<VW_ICMOInspectBillList> VW_ICMOInspectBillList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            #region 不指定列类型

            modelBuilder.Entity<ICMODaily>(entity =>
            {
                entity.HasKey(e => e.FID)
                    .HasName("PK__ICMODaily__008BF2DE");

                entity.HasIndex(e => e.FBillNo)
                    .HasName("IDX_OPBillNo")
                    .IsUnique();

                entity.HasIndex(e => e.FID)
                    .HasName("UQ__ICMODaily__01801717")
                    .IsUnique();

                entity.Property(e => e.FID)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FBillNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FBillTime)
                    .HasColumnType("datetime")
                    .HasConversion(v => v, v =>v.HasValue? new DateTime(v.Value.Year, v.Value.Month, v.Value.Day, v.Value.Hour, v.Value.Minute, v.Value.Second):v); 

                entity.Property(e => e.FBiller)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FCheckTime).HasColumnType("datetime");

                entity.Property(e => e.FChecker)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FCloseTime).HasColumnType("datetime");

                entity.Property(e => e.FClosed).HasDefaultValueSql("((0))");

                entity.Property(e => e.FCloser)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FCommitAuxQty)
                    .HasColumnType("decimal(28, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FDate).HasColumnType("datetime")
                    .HasConversion(v=>v,v=>new DateTime(v.Year,v.Month,v.Day,v.Hour,v.Minute,v.Second));
                    

                entity.Property(e => e.FEntryID).HasDefaultValueSql("((1))");

                entity.Property(e => e.FFailAuxQty)
                    .HasColumnType("decimal(38, 10)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FFinishAuxQty)
                    .HasColumnType("decimal(28, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FMOBillNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FMachineID).HasDefaultValueSql("((0))");

                entity.Property(e => e.FNote)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FOperNote)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FPassAuxQty)
                    .HasColumnType("decimal(38, 10)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FPlanAuxQty)
                    .HasColumnType("decimal(28, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FSrcID)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FTranType).HasDefaultValueSql("((851))");

                entity.Property(e => e.FWorkCenterID).HasDefaultValueSql("((0))");

                entity.Property(e => e.FWorker)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            #endregion


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}