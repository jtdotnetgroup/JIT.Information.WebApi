using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class t_equipment : Entity<int>
    {
        [NotMapped]
        public override int Id
        {
            get { return FInterID; }
            set { Id = FInterID; }
        }
        [Key]
        public int FInterID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public int FType { get; set; }
        public int FWorkCenterID { get; set; }
        public int FStatus { get; set; }
        public int FDayWorkHours { get; set; }
        public int FMaxWorkHours { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeletionTime { get; set; }
        public long DeleterUserId { get; set; }
        public long CreatorUserId { get; set; }
        public int FLift { get; set; }
        public int FResidualLife { get; set; }
        public decimal FRunsRate { get; set; }
        public int FSwichTime { get; set; }
        public int FTimeUnit { get; set; }

    }
}
