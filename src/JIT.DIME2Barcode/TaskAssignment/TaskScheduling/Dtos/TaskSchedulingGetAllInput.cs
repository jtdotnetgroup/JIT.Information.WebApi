using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.TaskAssignment.TaskScheduling.Dtos
{
    public class TaskSchedulingGetAllInput:JITPagedResultRequestDto
    { 

        [DisplayName("任务单号")]
        public string 任务单号 { get; set; }
        [DisplayName("状态")]
        public string 状态 { get; set; }
        [DisplayName("销售订单")]
        public string 销售订单 { get; set; }
        [DisplayName("车间")]
        public string 车间 { get; set; }
        [DisplayName("产品编码")]
        public string 产品编码 { get; set; }
        [DisplayName("产品名称")]
        public string 产品名称 { get; set; }
        [DisplayName("规格型号")]
        public string 规格型号 { get; set; }
        [DisplayName("单位")]
        public string 单位 { get; set; }
        [DisplayName("批号")]
        public string 批号 { get; set; }
        [DisplayName("计划生产数量")]
        public decimal 计划生产数量 { get; set; }
        [DisplayName("入库数量")]
        public decimal 入库数量 { get; set; }
        [DisplayName("计划开工日期")]
        public Nullable<System.DateTime> 计划开工日期 { get; set; }
        [DisplayName("计划完工日期")]
        public Nullable<System.DateTime> 计划完工日期 { get; set; }

    }
}