using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CommonTools;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JIT.DIME2Barcode.Entities
{
    public partial class Equipment : Entity<int>,ICreationAudited,IDeletionAudited
    {
        [NotMapped]
        public override int Id { get; set; }

        [Key]
        public int FInterID { get; set; }
        //资源编号
        public string FNumber { get; set; }
        //名称
        public string FName { get; set; }
        //类型
        public PublicEnum.EquipmentType FType { get; set; }
        //工作中心ID
        public Nullable<int> FWorkCenterID { get; set; }
        //状态
        public int FStatus { get; set; }
        //日工作时长
        public Nullable<TimeSpan> FDayWorkHours { get; set; }
        //最大工作时长
        public Nullable<TimeSpan> FMaxWorkHours { get; set; }
        //备注
        public string Note { get; set; }

        //时间单位
        public PublicEnum.TimeUnit FTimeUnit { get; set; }

        //切换时间
        public DateTime FSwichTime { get; set; }

        //使用寿命
        public int FLift { get; set; }
        //剩余寿命
        public int FResidualLife { get; set; }

        //产能系数
        public decimal FRunsRate { get; set; }

        [ForeignKey("FWorkCenterID")]
        public OrganizationUnit WorkCenter { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public long? CreatorUserId { get; set; }
    }
}