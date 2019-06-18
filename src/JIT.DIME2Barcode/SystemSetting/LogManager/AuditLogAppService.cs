using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.AppService;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.LogManager.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.SystemSetting.LogManager
{
    public class AuditLogAppService:BaseAppService
    {

         
        /// <summary>
        /// 异常查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AbpauditlogsDto>> GetAll(AbpauditlogsGetAllInput input)
         {

             

            var query = LogsRepository.GetAll();

            //一开始加载查询所有
            if (input.StartTime == null && input.EndTime == null && string.IsNullOrEmpty(input.Message)&& !input.Exception)
            {
                query = query;
            }     
            //根据时间查询
            else if (input.StartTime != null && input.EndTime != null && string.IsNullOrEmpty(input.Message)&& !input.Exception)
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime);
            }
            //时间加异常查询
            else if (input.StartTime != null && input.EndTime != null && !string.IsNullOrEmpty(input.Message)&& !input.Exception)
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime && p.Exception.Contains(input.Message));
            }
            //时间加异常查询加异常不为空
            else if (input.StartTime != null && input.EndTime != null && !string.IsNullOrEmpty(input.Message)&& input.Exception)
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime && p.Exception.Contains(input.Message)&& p.Exception!=null);
            }
            //时间加异常不为空
            else if (input.StartTime != null && input.EndTime != null && string.IsNullOrEmpty(input.Message) && input.Exception)
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime  && p.Exception != null);
            }
            //只查异常的条件
            else if (input.StartTime == null && input.EndTime == null && string.IsNullOrEmpty(input.Message) && input.Exception)
            {
                query = query.Where(p => p.Exception != null);
            }
            //异常内容+异常不为空
            else if(input.StartTime == null && input.EndTime == null && !string.IsNullOrEmpty(input.Message) && input.Exception)
            {
                query = query.Where(p =>  p.Exception.Contains(input.Message) && p.Exception != null);
            }
            //查询异常的内容
            else
            {
                query = query.Where(p => p.Exception.Contains(input.Message.Trim()));
            }
            var data = query.OrderBy(p => p.ExecutionTime).PageBy(input).ToList();
            var count = await query.CountAsync(); 
            var list = data.MapTo<List<AbpauditlogsDto>>();
            return new PagedResultDto<AbpauditlogsDto>(count, list);
        }

    }
}