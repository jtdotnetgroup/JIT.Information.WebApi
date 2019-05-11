using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace JIT.DIME2Barcode.TaskAssignment.TaskScheduling.Dtos
{
    [AutoMapFrom(typeof(Entities.VW_MOBillList))]
    public class TaskSchedulingDto:EntityDto
    {
        public string 任务单号 { get; set; }
        public string 状态 { get; set; }
        public string 销售订单 { get; set; }
        public string 车间 { get; set; }
        public string 产品编码 { get; set; }
        public string 产品名称 { get; set; }
        public string 规格型号 { get; set; }
        public string 单位 { get; set; }
        public string 批号 { get; set; }
        public decimal 计划生产数量 { get; set; }
        public decimal 入库数量 { get; set; }
        public Nullable<System.DateTime> 计划开工日期 { get; set; }
        public Nullable<System.DateTime> 计划完工日期 { get; set; }
        public decimal? 实际入库 { get; set; }
        public int FMOInterID { get; set; }
        public short FStatus { get; set; }
    }
}