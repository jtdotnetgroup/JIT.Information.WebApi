using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using JIT.DIME2Barcode.BackgroudJobs;
using JIT.DIME2Barcode.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService.SysTask
{
    /// <summary>
    /// 任务调度
    /// </summary>
    public class Sys_TaskAppService : BaseAppService
    {
        /// <summary>
        /// 获取所有任务调度
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Sys_Task>> Sys_TaskList(Sys_Task mSysTask)
        {
            var data = JIT_Sys_Task.GetAll().Where(w => w.TaskState != -1);
            var result = await data.Where(w =>
                    w.TaskName.Contains(mSysTask.TaskName) && w.TaskType.Contains(mSysTask.TaskType) &&
                    (mSysTask.TaskState.Equals(0) || w.TaskState.Equals(mSysTask.TaskState)))
                .ToListAsync();
            return result.MapTo<List<Sys_Task>>();
        }

        /// <summary>
        /// 任务调度添加以及修改
        /// </summary>
        public async void UpdOrAdd(Sys_Task mSysTask)
        {
            //mSysTask.Id = mSysTask.TaskId;
            mSysTask.LastSyncTime = DateTime.Now;
            mSysTask.LastSyncUserId = AbpSession.UserId.ToString();
            await JIT_Sys_Task.InsertOrUpdateAsync(mSysTask);
            //new AllTask().SetTask(mSysTask.TaskId, GetAll());
        }

        /// <summary>
        /// 获取任务调度明细
        /// </summary>
        /// <param name="mSysTask"></param>
        /// <returns></returns>
        public async Task<Sys_Task> Get(Sys_Task mSysTask)
        {
            return await JIT_Sys_Task.GetAsync(mSysTask.TaskId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Sys_Task> GetAll()
        {
            var result = JIT_Sys_Task.GetAll().Where(w => w.TaskState != -1).ToList();
            return result.MapTo<List<Sys_Task>>();
        }
    }
}
