using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using CommonTools;
using JIT.DIME2Barcode.AppService.SysTask;
using JIT.DIME2Barcode.Entities;
using Quartz;

namespace JIT.DIME2Barcode.BackgroudJobs
{
    #region 定义所有任务
    /// <summary>
    /// 定义所有任务
    /// </summary>
    public class AllTask
    { 
        /// <summary>
        /// 所有任务
        /// </summary> 
        public List<Sys_Task> TaskList = null;

        /// <summary>
        /// 构造方法一开始执行
        /// </summary>
        public AllTask()
        {
            TaskList = null;
        }

        /// <summary>
        /// 所有任务类型
        /// </summary>
        public enum TaskType
        {
            [DisplayName("当前时间"), Description("CurrentTime")]
            CurrentTime
        }

        /// <summary>
        /// 设置任务
        /// </summary>
        public void SetTask(int taskId = 0, List<Sys_Task> aSysTasks = null)
        {
            TaskList = aSysTasks;
            //开始设置任务
            foreach (var item in TaskList.Where(w => taskId.Equals(0) || w.TaskId.Equals(taskId)))
            {
                if (TaskType.CurrentTime.ToDescription() == item.TaskType)
                {
                    new QuartzHelper<CurrentTime>().Command(item.TaskState, item.TaskCorn);
                }
            }
        }

        /// <summary>
        /// 删除任务   --- 用编码删除
        /// </summary>
        public void DelTask(int taskId = 0)
        {
            foreach (var item in TaskList.Where(w => taskId.Equals(0) || w.TaskId.Equals(taskId)))
            {
                //if (TaskType.CurrentTime.ToDescription() == item.TaskType)
                //{
                //    new QuartzHelper<CurrentTime>().Command(item.TaskState, item.TaskCorn);
                //}
            }
        }

    }
    #endregion

    #region 所有任务
    /// <summary>
    /// 当前时间
    /// </summary>
    [PersistJobDataAfterExecution]  //保存执行状态
    [DisallowConcurrentExecution]   //不允许并发执行
    public class CurrentTime : IJob
    {
        /// <summary>
        /// 获取数据或者业务处理等等
        /// </summary>
        public CurrentTime()
        {

        } 
        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("executed..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            Console.WriteLine("executed..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            throw new NotImplementedException();
        }
    }
    #endregion
}
