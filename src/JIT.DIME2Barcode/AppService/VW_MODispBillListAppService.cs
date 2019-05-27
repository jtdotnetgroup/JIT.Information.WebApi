using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions; 
using JIT.JIT.TaskAssignment.VW_MODispBillList.Dtos;

namespace JIT.JIT.TaskAssignment.VW_MODispBillList
{
    /// <summary>
    /// 派工任务
    /// </summary>
    public class VW_MODispBillListAppService : ApplicationService
    {
        public IRepository<DIME2Barcode.Entities.VW_MODispBillList, string> Repository { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList> GetAll(VW_MODispBillListGetAllInput input)
        {
            var query = Repository.GetAll().Where(w => w.操作者 == AbpSession.UserId.ToString() && w.FStatus == input.FStatus);
            query = input.FStatus == 0 ? query.Where(p => p.FStatus == input.FStatus) : query.Where(p => p.FStatus > 0);
            query = input.FClosed.HasValue ? query.Where(p => p.FClosed == input.FClosed) : query;

            var data = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var count = query.Count();

            var list = data.MapTo<List<DIME2Barcode.Entities.VW_MODispBillList>>();

            return new PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList>(count, list);
        }
    }
}
