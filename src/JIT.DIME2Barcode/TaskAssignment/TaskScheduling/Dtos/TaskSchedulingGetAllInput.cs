using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;

namespace JIT.DIME2Barcode.TaskAssignment.TaskScheduling.Dtos
{
    public class TaskSchedulingGetAllInput:JITPagedResultRequestDto
    {
        [DisplayName("产品名称")]
        public string 产品名称 { get; set; }
        [DisplayName("任务单号")]
        public string 任务单号 { get; set; }
        [DisplayName("计划数量")]
        public decimal 计划生产数量 { get; set; }
        [DisplayName("入库数量")]
        public decimal 入库数量 { get; set; }
    }
}