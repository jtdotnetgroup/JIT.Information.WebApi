using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JIT.DIME2Barcode.Model;

namespace JIT.DIME2Barcode.TaskAssignment.Sys_Task.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class Sys_TaskAllInputDto: JITPagedResultRequestDto
    {  
        [DisplayName("任务编码")]
        public string TaskCode { get; set; } 
        [DisplayName("任务名称")]
        public string TaskName { get; set; } 
        [DisplayName("任务类型")]
        public string TaskType { get; set; } 
        [DisplayName("任务描述")]
        public string TaskBz { get; set; }
        /// <summary>
        /// 任务状态 暂停，未更新，(执行中/已更新)，已结束
        /// </summary>
        [DisplayName("任务状态")]
        public int TaskState { get; set; } 
        [DisplayName("用户ID")]
        public string LastSyncUserId { get; set; } 
        [DisplayName("最后更新时间")]
        public DateTime LastSyncTime { get; set; }
        [DisplayName("执行Corn表达式")] 
        public string TaskCorn { get; set; } 
    }
}
