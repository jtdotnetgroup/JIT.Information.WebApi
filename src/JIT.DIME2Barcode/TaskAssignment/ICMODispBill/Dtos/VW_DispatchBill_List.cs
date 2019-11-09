﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class VW_DispatchBill_List:Entity<string>
    {
        [NotMapped]
        public override string Id{ get; set; }

        public DateTime FDate { get; set; }
       
        public string FID { get; set; }
        public string FMOBillNo { get; set; }
        public int FMOInterID { get; set; }
        public string FBillNo { get; set; }
        public DateTime? FBillTime { get; set; }
        public  string Machine { get; set; }
        public int FMachineID { get; set; }
        public string Worker { get; set; }
        public int FWorkerID { get; set; }
        public string FShift { get; set; }
        public decimal? FCommitAuxQty { get; set; }
        public decimal? FFinishAuxQty { get; set; }
        public decimal? FPassAuxQty { get; set; }
        public string FBiller { get; set; }

        public string UserName { get; set; }

        public string FItemName { get; set; }

        public int FShiftID { get; set; }
       
        public string dispFid { get; set; }
        public string FStatus { get; set; }
        public decimal? FPackQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Key]
        //[NotMapped] 
        [Key]
        public string GUID { get; set; }
    }
}