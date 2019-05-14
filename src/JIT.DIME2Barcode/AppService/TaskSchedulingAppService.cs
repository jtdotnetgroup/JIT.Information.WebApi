using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.TaskAssignment.TaskScheduling.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.EntityFrameworkCore.Repositories;
using CommonTools;

namespace JIT.DIME2Barcode.TaskAssignment.TaskScheduling
{
    public class TaskSchedulingAppService:ApplicationService
    {
        public IRepository<Entities.VW_MOBillList,string> Repository { get; set; }

        public IRepository<Entities.ICMODaily,string> DRepository { get; set; }
        /// <summary>
        /// 查询所有任务单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<TaskSchedulingDto>> GetAll(TaskSchedulingGetAllInput input)
        {
            var query = Repository.GetAll();

            var count = await query.CountAsync();

            query = string.IsNullOrEmpty(input.Sorting)
                ? query.OrderBy(p => p.计划开工日期).PageBy(input)
                : query.OrderBy(input.Sorting).PageBy(input);

            var data = await query.ToListAsync();
            var list = data.MapTo<List<TaskSchedulingDto>>();

            return new PagedResultDto<TaskSchedulingDto>(count,list);
        }
        /// <summary>
        /// 根据任务单ID，查询排产信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataTable GetDetails(TaskScheduleDetailInput input)
        {
            var connection = Repository.GetDbContext().Database.GetDbConnection();
            var connStr = connection.ConnectionString;

            var spName = "GetMODailyList";

            var spParams=new Dictionary<string,object>();
            spParams.Add("@MOInterID",input.FMOInterID);

            var table = SQLStoredProcedureHelper.Exec(spName, connStr, spParams);

            return table;

        }


        
    }
}