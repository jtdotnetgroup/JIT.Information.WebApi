using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.VM_Inventory.Dtos;
using JIT.JIT.TaskAssignment.VW_MODispBillList.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 库存查询
    /// </summary>
    public class VM_InventoryAppService : BaseAppService
    {
        /// <summary>
        /// 列表
        /// </summary>
        [AbpAuthorize(ProductionPlanPermissionsNames.TouchPadStock)]
        public async Task<PagedResultDto<VM_Inventory>> GetAll(VM_InventoryGetAllInput input)
        {
            var query = JIT_VM_Inventory.GetAll();
            var data = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var list = data.MapTo<List<VM_Inventory>>();
            return new PagedResultDto<VM_Inventory>(query.Count(), list);
        }
    }
}
