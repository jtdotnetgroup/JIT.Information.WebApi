using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 任务调度执行记录表
    /// </summary>
    public class Sys_TaskRecord : Entity<int>
    {
        [NotMapped]
        public override int Id { get; set; }

        /// <summary>
        /// 唯一ID,主键ID
        /// </summary>
       [Key]
        public int TaskRecordId { get; set; }
        /// <summary>
        /// 任务调度定义ID
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 创建时间   执行任务时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 运行状态 成功，异常，失败
        /// </summary>
        public string TaskState { get; set; }
        /// <summary>
        /// 运行明细 用来记录异常信息
        /// </summary>
        public string FunctionDetailed { get; set; }
        /// <summary>
        /// 执行Corn表达式
        /// </summary>
        public string TaskCorn { get; set; } 
    }
}
