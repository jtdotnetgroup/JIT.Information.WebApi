using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using CommonTools;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 
    /// </summary>
    public class VM_ICMOInspectBillEDAppService:BaseAppService
    {
        /// <summary>
        /// 
        /// </summary>
        public async Task<PagedResultDto<DIME2Barcode.Entities.VM_ICMOInspectBillED>> GetAll(
            PagedAndSortedResultRequestDto input)
        {
            var query = JIT_VM_ICMOInspectBillED.GetAll().Where(A =>
                A.FStatus >= PublicEnum.ICMODispBillState.已检验.EnumToInt() && A.FYSQty > 0);

            var data = await query.PageBy(input).ToListAsync();
            var list = data.MapTo<List<DIME2Barcode.Entities.VM_ICMOInspectBillED>>();
            return new PagedResultDto<DIME2Barcode.Entities.VM_ICMOInspectBillED>(query.Count(), list);
        }
    }
}
