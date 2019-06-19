using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace CommonTools
{
    /// <summary>
    /// 定时任务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuartzHelper<T> where T : IJob
    {
        #region 基本配置

        #region 公共变量 
        public ISchedulerFactory Factory = new StdSchedulerFactory();
        public IScheduler Scheduler = null;
        public IJobDetail Job = null; 
        #endregion

        /// <summary>
        /// 一开始执行
        /// </summary>
        public QuartzHelper()
        {
            Scheduler = Factory.GetScheduler();
            Scheduler = Factory.GetScheduler();
            Job = JobBuilder.Create<T>().Build();
        }
        #endregion

        #region 全部方法
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="com"></param>
        /// <param name="cron"></param>
        public void Command(int com, string cron)
        {
            switch (com)
            {
                case 1:
                    Start(cron);
                    break;
                case 2:
                    Stop();
                    break;
                case 3:
                    Del();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        public void Start(string cronExpression)
        {
            Del();
            ExecuteByCron(cronExpression);
            Scheduler.Start();
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            Scheduler.Shutdown(false);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        public void Del()
        {
            Scheduler.Clear();
        }
        #endregion

        #region 定制方法
        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：毫秒)</param>
        public void ExecuteInterval(int seconds)
        { 
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
                .Build();
            Scheduler.ScheduleJob(Job, trigger); 
        }
        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public void ExecuteByCron(string cronExpression)
        {
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithCronSchedule(cronExpression)
                .Build();
            Scheduler.ScheduleJob(Job, trigger);
        }
        //http://cron.qqe2.com/ 
        //通过这个生成器,您可以在线生成任务调度比如Quartz的Cron表达式,对Quartz Cron 表达式的
        //https://www.cnblogs.com/pingming/p/4999228.html
        //cron expressions 整体上还是非常容易理解的，只有一点需要注意："?"号的用法，看下文可以知道“？”可以用在 day of month 和 day of week中，他主要是为了解决如下场景，如：每月的1号的每小时的31分钟，正确的表达式是：* 31 * 1 * ？，而不能是：* 31 * 1 * *，因为这样代表每周的任意一天。
        //由7段构成：秒 分 时 日 月 星期 年（可选） 
        //"-" ：表示范围  MON-WED表示星期一到星期三 
        //"," ：表示列举 MON,WEB表示星期一和星期三 
        //"*" ：表是“每”，每月，每天，每周，每年等 
        //"/" :表示增量：0/15（处于分钟段里面） 每15分钟，在0分以后开始，3/20 每20分钟，从3分钟以后开始 
        //"?" :只能出现在日，星期段里面，表示不指定具体的值 
        //"L" :只能出现在日，星期段里面，是Last的缩写，一个月的最后一天，一个星期的最后一天（星期六） 
        //"W" :表示工作日，距离给定值最近的工作日 
        //"#" :表示一个月的第几个星期几，例如："6#3"表示每个月的第三个星期五（1=SUN...6=FRI,7=SAT）
        #endregion
    }
    #region 任务执行例
    //public class MyJob : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        Console.WriteLine("executed..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    //    }
    //} 
    #endregion

}
