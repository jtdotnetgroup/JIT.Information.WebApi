using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.Sys_Task.Dtos
{
    /// <summary>
    /// 任务调度运行记录明细
    /// </summary>
    public class Sys_TaskRecordAllInputDto: PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 任务调度定义表ID
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        public string TaskState { get; set; }
        /// <summary>
        /// 运行明细,运行情况
        /// </summary>
        public string FunctionDetailed { get; set; }
    }
}
