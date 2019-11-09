using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using JetBrains.Annotations;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillGetAllInput:JITPagedResultRequestDto
    {
        [DisplayName("日期")]
        public  DateTime? FDate { get; set; }
        
        [DisplayName("任务单号")]
        [CanBeNull] public string FMOBillNo { get; set; }

        [DisplayName("派工单号")]
        public string FBillNo { get; set; }

        [DisplayName("机台号")]
        public string Machine { get; set; }

        [DisplayName("操作员")]
        public string Worker { get; set; }

        [DisplayName("班次")]
        public string FShift { get; set; }

        [DisplayName("状态")]
        public string FStatus { get; set; }

    }

    public class ICMODispBillGetAllInputTwo : JITPagedResultRequestDto
    {
        public string FSrcID { get; set; }
    }

    public class ICMODispBill_Daily_GetAllListInput
    {
        [Required]
        public DateTime[] DatelList { get; set; }
        [Required]
        public string[] FMOBillNos { get; set; }
    }
}