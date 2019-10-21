﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyCreatDto:EntityDto<string>
    {
        [NotMapped]
        public new string Id { get; set; }
        
        public int FMOInterID { get; set; }
        [Required]
        public string FMOBillNo { get; set; }

        public string FWorkCenterName { get; set; }

        public DailyDto[] Dailies { get; set; }
    }
}