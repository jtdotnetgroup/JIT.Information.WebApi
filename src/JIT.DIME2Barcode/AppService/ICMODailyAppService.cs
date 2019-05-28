using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.Runtime.Validation;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.EventsAndEventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JIT.DIME2Barcode.TaskAssignment
{
    /// <summary>
    ///     任务排产接口服务
    /// </summary>

    public class ICMODailyAppService : ApplicationService
    {
        public IEventBus EventBus { get; set; }

        //任务单仓储
        public IRepository<ICMO> MRepository { get; set; }

        //任务计划单仓储
        public IRepository<ICMOSchedule, string> SRepository { get; set; }

        //日计划单视图仓储
        public IRepository<VW_ICMODaily, string> VRepository { get; set; }
        public IRepository<Entities.VW_MOBillList, string> MRepository { get; set; }
        public IRepository<VW_ICMODaily_Group_By_Day,string> GRepository { get; set; }
        public IRepository<OrganizationUnit> ORepository { get; set; }//组织架构仓储
        public IRepository<EqiupmentShift,int> EsRepository { get; set; }

        //设备档案仓储
        public IRepository<Equipment,int > ERepository { get; set; }

        //日计划单仓储
        public IRepository<Entities.ICMODaily, string> Repository { get; set; }
         

        public IRepository<VW_Group_ICMODaily, string> GrRepository { get; set; }

        /// <summary>
        ///     任务派工界面列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<VW_ICMODailyDto>> GetAll(ICMODailyGetAllInput input)
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

        /// <summary>
        ///     生成日计划单接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VW_ICMODailyDto> Create(ICMODailyCreatDto input)
        {
            var dailyList = await Repository.GetAll().Where(p => p.FMOBillNo == input.FMOBillNo && p.FMOInterID == input.FMOInterID).ToListAsync();
            var fsrcId = ""; //任务计划单FID
            decimal? totoalPlan = 0;//计划单排产数

            var equipmentList =await ERepository.GetAll().Where(p => p.FType == PublicEnum.EquipmentType.设备).ToListAsync();
            var orgs = await ORepository.GetAll().Where(p => p.OrganizationType == PublicEnum.OrganizationType.车间).ToListAsync();

            var eqShifts = await EsRepository.GetAll().ToListAsync();

            if (icmo != null)
            {
                var schedule = GetOrCreateSchedule(icmo);


                Entities.ICMODaily insertUpdateObj = null;

                foreach (var dailyItem in input.Dailies)
                {
                    totoalPlan += item.FPlanAuxQty;
                    var index = (input.Dailies.IndexOf(item) + dailyList.Count + 1).ToString("000");
                    insertUpdateObj = new Entities.ICMODaily()
                    {
                        FMachineID = item.FMachineID,
                        FWorkCenterID = item.FWorkCenterID,
                        FShift = item.FShift,
                        FID = Guid.NewGuid().ToString(),
                        FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index,//任务计划单号
                        FMOInterID = input.FMOInterID,//任务单号
                        FMOBillNo = input.FMOBillNo,//任务单号
                        FBiller = this.AbpSession.UserId.ToString(),//当前登录用户
                        FSrcID = fsrcId,
                        FPlanAuxQty = item.FPlanAuxQty,
                        FBillTime = DateTime.Now,
                        FDate = item.FDate.Date
                    };

                    var entity = Repository.Insert(insertUpdateObj);
                }

                //更新计划单
                SRepository.Update(schedule);
                return schedule.Dailies.Count;
            }
            else
            {
                throw new AbpValidationException(string.Format("任务单：{0}不存在", input.FMOBillNo));
            }

            return 0;
        }

        protected async Task InsertOrUpdateICMOSchedul(ICMODailyCreatedtEventData eventData)
        {
            var entity = await SRepository.GetAll().SingleOrDefaultAsync(p =>
                p.FID == eventData.FSrcID);

            if (entity == null)
            {


                entity = new ICMOSchedule
                {
                    FID = eventData.FSrcID,
                    FBillTime = DateTime.Now,
                    FBillNo = "SC-" + eventData.FMOBillNo,
                    FBiller = eventData.FBiller,
                    FPlanAuxQty = eventData.FPlanAuxQty
                };
                SRepository.Insert(entity);
            }
            else
            {
                entity.FPlanAuxQty = eventData.FPlanAuxQty;

                SRepository.Update(entity);
            }
        }


        public async Task<Array> GetDialyListByFMOInterID(ICMODailyGetAllInput input)
        {
            var query = Repository.GetAll().Where(p => p.FMOInterID == input.FMOInterID);

            var linq = from a in query
                select new {a.FDate,a.FMachineID,a.FPlanAuxQty,a.FCommitAuxQty};

            var data = await linq.ToListAsync();

            return data.ToArray();

        }
    }
}