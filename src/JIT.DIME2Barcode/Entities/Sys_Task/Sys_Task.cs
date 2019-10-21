using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 任务调度定义表
    /// </summary>
    public class Sys_Task : Entity<int>
    {
        [NotMapped]
        public override int Id
        {
            get { return TaskId; }
            set { Id = TaskId; }
        }

        /// <summary>
        /// 唯一ID,主键ID
        /// </summary>
        [Key]
        public int TaskId { get; set; }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskBz { get; set; }
        /// <summary>
        /// 任务状态 暂停，未更新，(执行中/已更新)，已结束
        /// </summary>
        public int TaskState { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string LastSyncUserId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastSyncTime { get; set; }
        /// <summary>
        /// 执行Corn表达式
        /// </summary>
        public string TaskCorn { get; set; }
    }
}
