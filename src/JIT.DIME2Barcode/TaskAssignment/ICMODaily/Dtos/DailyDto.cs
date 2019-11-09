﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class DailyDto
    {
        //计划日期
        public DateTime FDate { get; set; }
        //计划数量
        [Required]
        public decimal FPlanAuxQty { get; set; }
        //机台ID
        public int FMachineID { get; set; }
        //机台设备名称
        public string FMachineName { get; set; }
        //工作中心或车间
        public int FWorkCenterID { get; set; }
        //班次
        public string FShift { get; set; }

        public string fid { get; set; }
        //工序名称
        public string FOperID { get; set; }

        public decimal PackQty { get; set; }
    }
}