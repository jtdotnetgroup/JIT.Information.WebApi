using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMOInspectBillList.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    public class ICMOInspectBillAppService:ApplicationService
    {
        public IRepository<ICMOInspectBill,string> Repository { get; set; }
        public IRepository<VW_ICMOInspectBillList,string> VRepository { get; set; }

        public async Task<PagedResultDto<VW_ICMOInspectBillListDto>> GetAll(VW_ICMOInspectBillListGetAllInput input)
        {
            var query = VRepository.GetAll().OrderBy(p=>p.派工单号);
            var count = await query.CountAsync();

            var data = await query.PageBy(input).ToListAsync();

            var list = data.MapTo<List<VW_ICMOInspectBillListDto>>();

            return new PagedResultDto<VW_ICMOInspectBillListDto>(count,list);
        }
        
    }
}