using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
using JIT.JIT.TaskAssignment.VW_MODispBillList.Dtos;

namespace JIT.TaskAssignment
{
    /// <summary>
    /// 派工任务
    /// </summary>
    public class VW_MODispBillListAppService : ApplicationService
    {
        public IRepository<VW_MODispBillList> Repository { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public PagedResultDto<VW_MODispBillListDto> GetAll(VW_MODispBillListGetAllInput input)
        {
            var query = Repository.GetAll().Where(w => w.操作者 == input.操作者 && w.FStatus == input.FStatus);
            query = input.FStatus == 0 ? query.Where(p => p.FStatus == input.FStatus) : query.Where(p => p.FStatus > 0);
            query = input.FClosed.HasValue ? query.Where(p => p.FClosed == input.FClosed) : query;

            input.Sorting= string.IsNullOrEmpty(input.Sorting) ? input.Sorting = "FID":input.Sorting;

            var data = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var count = query.Count();

            var list = data.MapTo<List<VW_MODispBillListDto>>();

            return new PagedResultDto<VW_MODispBillListDto>(count, list);
        }
    }
}
