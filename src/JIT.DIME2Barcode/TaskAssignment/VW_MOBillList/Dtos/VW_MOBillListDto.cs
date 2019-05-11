using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.VW_MOBillList.Dtos
{
    public class VW_MOBillListDto
    {
        public string 任务单号 { get; set; }
        public int 状态 { get; set; }
        public string 销售订单 { get; set; }
        public string 车间 { get; set; }
        public string 产品编码 { get; set; }
        public string 产品名称 { get; set; }
        public string 规格型号 { get; set; }
        public string 单位 { get; set; }
        public string 批号 { get; set; }
        public float 计划生产数量 { get; set; }
        public float? 入库数量 { get; set; }
        public DateTime 计划开工日期 { get; set; }
        public DateTime 计划完工日期 { get; set; }
        public string FMOInterID { get; set; }
        public string FStatus { get; set; }
    }
}