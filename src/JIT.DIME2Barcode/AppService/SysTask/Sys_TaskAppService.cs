using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using JIT.DIME2Barcode.Entities; 
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
        public async Task<List<Sys_Task>> GetAll()
        {
            
            var data = await JIT_Sys_Task.GetAll().Where(w => w.TaskState != -1).ToListAsync();
            return data.MapTo<List<Sys_Task>>();
        }
        /// <summary>
        /// 任务调度添加以及修改
        /// </summary>
        public async void UpdOrAdd(Sys_Task mSysTask)
        {
            var entity = await JIT_Sys_Task.GetAll()
                             .FirstOrDefaultAsync(s => s.Id.Equals(mSysTask.Id)) ?? mSysTask;
            entity.LastSyncTime = DateTime.Now;
            await JIT_Sys_Task.InsertOrUpdateAsync(entity);
        }
    }
}
