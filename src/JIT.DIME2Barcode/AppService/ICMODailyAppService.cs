using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.TaskAssignment
{
    public class ICMODailyAppService : AsyncCrudAppService<Entities.ICMODaily, VW_ICMODailyDto, string, ICMODailyGetAllInput, ICMODailyCreatDto, ICMODailyCreatDto, VW_ICMODailyDto, VW_ICMODailyDto>
    {
        //计划单仓储
        public IRepository<ICMOSchedule, string> SRepository { get; set; }
        //日计划单视图仓储
        public IRepository<VW_ICMODaily, string> VRepository { get; set; }

        public ICMODailyAppService(IRepository<Entities.ICMODaily, string> repository) : base(repository)
        {
        }

        public override async Task<PagedResultDto<VW_ICMODailyDto>> GetAll(ICMODailyGetAllInput input)
        {
            var query = VRepository.GetAll();

            var count = await query.CountAsync();

            query = string.IsNullOrEmpty(input.Sorting)
                ? query.OrderBy(p => p.日期).PageBy(input)
                : query.OrderBy(input.Sorting).PageBy(input);

            var data = await query.ToListAsync();

            var list = data.MapTo<List<VW_ICMODailyDto>>();

            return new PagedResultDto<VW_ICMODailyDto>(count, list);
        }

        public override async Task<VW_ICMODailyDto> Create(ICMODailyCreatDto input)
        {
            var scheduld = SRepository.GetAll()
                .SingleOrDefault(p => p.FMOBillNo == input.FMOBillNo && p.FMOInterID == input.FMOInterID);

            var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (scheduld == null)
            {
                scheduld = new ICMOSchedule()
                {
                    FMOBillNo = input.FMOBillNo,
                    FMOInterID = input.FMOInterID,
                    FID = Guid.NewGuid().ToString(),
                    FBillNo = "SC" + datetime,
                    FBillTime = DateTime.Now,
                    FBiller = this.AbpSession.UserId.ToString()
                };

                scheduld = await SRepository.InsertAsync(scheduld);
            }

            var dailyList = await Repository.GetAll().Where(p => p.FSrcID == scheduld.FID).ToListAsync();

            foreach (var item in input.Dailies)
            {
                var daily = dailyList.SingleOrDefault(p =>
                    p.FShift == item.FShift && p.FMachineID == item.FMachineID &&
                    item.FWorkCenterID == p.FWorkCenterID && item.FDate == p.FDate);

                Entities.ICMODaily insertOrupdateObj = null;

                if (daily != null)
                {
                    insertOrupdateObj = daily;
                    insertOrupdateObj.FPlanAuxQty = item.FPlanAuxQty;
                    Repository.Update(insertOrupdateObj);
                }
                else
                {

                    var index = (Array.IndexOf(input.Dailies, daily) + dailyList.Count + 1).ToString("000");
                    insertOrupdateObj = new Entities.ICMODaily()
                    {
                        FMachineID = daily.FMachineID,
                        FWorkCenterID = daily.FWorkCenterID,
                        FShift = daily.FShift,
                        FID = Guid.NewGuid().ToString(),
                        FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index,//任务计划单号
                        FMOInterID = input.FMOInterID,//任务单号
                        FMOBillNo = input.FMOBillNo,//任务单号
                        FBiller = this.AbpSession.UserId.ToString(),//当前登录用户
                        FSrcID = scheduld.FID,
                        FPlanAuxQty = item.FPlanAuxQty,
                        FBillTime = DateTime.Now,
                    };

                    await Repository.InsertAsync(insertOrupdateObj);
                }
            }

            return null;
        }
    }
}