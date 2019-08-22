using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CommonTools;
using JIT.DIME2Barcode.AppService;
using JIT.DIME2Barcode.Permissions;
using JIT.JIT.TaskAssignment.VW_MODispBillList.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JIT.JIT.TaskAssignment.VW_MODispBillList
{
    /// <summary>
    /// 派工任务
    /// </summary>
    public class VW_MODispBillListAppService : BaseAppService
    {
        /// <summary>
        /// 列表
        /// </summary>
        [AbpAuthorize(ProductionPlanPermissionsNames.TouchPadDispatchedWork)]
        public PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList> GetAll(VW_MODispBillListGetAllInput input)
        {
            var query = JIT_VW_MODispBillList.GetAll().Where(w => w.操作者 == AbpSession.UserId && w.FStatus == input.FStatus);
            query = input.FStatus == 0 ? query.Where(p => p.FStatus == input.FStatus) : query.Where(p => p.FStatus > 0);
            query = input.FClosed.HasValue ? query.Where(p => p.FClosed == input.FClosed) : query;

            var data = query.OrderByDescending(p=>p.FStartTime).OrderByDescending(p=>p.FBillTime).PageBy(input).ToList();
          
            var count = query.Count();

            var list = data.MapTo<List<DIME2Barcode.Entities.VW_MODispBillList>>();

            return new PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList>(count, list);
        }

        /// <summary>
        /// 待检验
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TouchPadIPQC)]
        [HttpGet]
        public PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList> JYZGetAll(VW_MODispBillListGetAllInput input)
        {
            var query = JIT_VW_MODispBillList.GetAll().Where(w => w.FStatus == input.FStatus);
            query = input.FStatus == PublicEnum.ICMODispBillState.已检验.EnumToInt()
                ? query.Where(w => w.操作者 == AbpSession.UserId) : query;
            query = input.FStatus == 0 ? query.Where(p => p.FStatus == input.FStatus) : query.Where(p => p.FStatus > 0);
            query = input.FClosed.HasValue ? query.Where(p => p.FClosed == input.FClosed) : query;

            var data = query.OrderByDescending(p=>p.FStartTime).OrderByDescending(p=>p.FBillTime).PageBy(input).ToList();

            var count = query.Count();

            var list = data.MapTo<List<DIME2Barcode.Entities.VW_MODispBillList>>();

            return new PagedResultDto<DIME2Barcode.Entities.VW_MODispBillList>(count, list);
        }
    }
}
