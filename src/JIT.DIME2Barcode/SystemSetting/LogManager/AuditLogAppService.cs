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

         

        public async Task<PagedResultDto<AbpauditlogsDto>> GetAll(AbpauditlogsGetAllInput input)
        {

            var query = LogsRepository.GetAll();

            if (input.StartTime == null && input.EndTime == null && string.IsNullOrEmpty(input.Message))
            {
                query = query;
            }
            else if (input.StartTime != null && input.EndTime != null && string.IsNullOrEmpty(input.Message))
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime);
            }
            else if (input.StartTime != null && input.EndTime != null && !string.IsNullOrEmpty(input.Message))
            {
                query = query.Where(p => p.ExecutionTime >= input.StartTime && p.ExecutionTime <= input.EndTime && p.Exception.Contains(input.Message));
            }
            else
            {
                query = query.Where(p => p.Exception.Contains(input.Message.Trim()));
            }
            var data = query.OrderBy(p => p.Id).PageBy(input).ToList();
            var count = await query.CountAsync(); 
            var list = data.MapTo<List<AbpauditlogsDto>>();
            return new PagedResultDto<AbpauditlogsDto>(count, list);
        }

    }
}