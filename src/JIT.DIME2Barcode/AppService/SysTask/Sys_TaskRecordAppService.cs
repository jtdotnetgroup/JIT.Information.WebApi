using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.Sys_Task.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService.SysTask
{
    /// <summary>
    /// 任务调度运行记录
    /// </summary>
    public class Sys_TaskRecordAppService : BaseAppService
    {
        /// <summary>
        /// 获取所有任务调度运行记录
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_TaskRecord>> GetAll(Sys_TaskRecordAllInputDto input)
        {
            var data = await JIT_Sys_TaskRecord.GetAll()
                .Where(w => (input.TaskID.Equals(0) || w.TaskId.Equals(input.TaskID)) &&
                            (input.TaskState.Equals("") || w.TaskState.Equals(input.TaskState)) &&
                            (input.FunctionDetailed.Equals("") || w.FunctionDetailed.Contains(input.FunctionDetailed))
                )
                .OrderBy(o => o.CreateTime).PageBy(input).ToListAsync();
            return data.MapTo<List<Sys_TaskRecord>>();
        }
        /// <summary>
        /// 任务调度运行记录添加
        /// </summary>
        [Audited]
        public async void Add(Sys_TaskRecord mRecord)
        {
            try
            {
                var entity = await JIT_Sys_TaskRecord.GetAll()
                                 .FirstOrDefaultAsync(s => s.Id.Equals(mRecord.Id)) ?? mRecord;
                entity.CreateTime = DateTime.Now;
                await JIT_Sys_TaskRecord.InsertAsync(entity);
                EX(0,"系统提示","添加成功！");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                EX(0, "系统提示", "添加失败！" + e.Message);
            }
        }
    }
}
