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
        public ICMODailyAppService()
        {
            EventBus = NullEventBus.Instance;
        }

        public IEventBus EventBus { get; set; }

        //任务单仓储

        //任务计划单仓储
        public IRepository<ICMOSchedule, string> SRepository { get; set; }

        //日计划单视图仓储
        public IRepository<VW_ICMODaily, string> VRepository { get; set; }
        public IRepository<Entities.VW_MOBillList,string> MRepository { get; set; }

        //日计划单仓储
        public IRepository<Entities.ICMODaily, string> Repository { get; set; }

        public IRepository<VW_Group_ICMODaily, string> GrRepository { get; set; }

        /// <summary>
        ///     任务派工界面列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize("")]
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
        public async Task<int> Create(ICMODailyCreatDto input)
        {
            #region 弃用

            //var dailyList = await Repository.GetAll()
            //    .Where(p => p.FMOBillNo == input.FMOBillNo && p.FMOInterID == input.FMOInterID).ToListAsync();
            //var fsrcId = ""; //任务计划单FID
            //decimal? totoalPlan = 0; //计划单排产数

            //if (dailyList == null || dailyList.Count == 0)
            //{
            //    fsrcId = Guid.NewGuid().ToString();
            //}
            //else
            //{
            //    fsrcId = dailyList.First().FSrcID;
            //    totoalPlan += dailyList.Sum(p => p.FPlanAuxQty);
            //}

            ////插入日计划单
            //foreach (var item in input.Dailies)
            //{
            //    var daily = dailyList.SingleOrDefault(p =>
            //        p.FShift == item.FShift && p.FMachineID == item.FMachineID &&
            //        item.FWorkCenterID == p.FWorkCenterID && item.FDate == p.FDate);


            //    Entities.ICMODaily insertUpdateObj = null;

            //    if (daily != null)
            //    {
            //        totoalPlan -= daily.FPlanAuxQty;
            //        totoalPlan += item.FPlanAuxQty;
            //        insertUpdateObj = daily;
            //        insertUpdateObj.FPlanAuxQty = item.FPlanAuxQty;
            //        Repository.Update(insertUpdateObj);
            //    }
            //    else
            //    {
            //        totoalPlan += item.FPlanAuxQty;
            //        var index = (input.Dailies.IndexOf(item) + dailyList.Count + 1).ToString("000");
            //        insertUpdateObj = new Entities.ICMODaily
            //        {
            //            FMachineID = item.FMachineID,
            //            FWorkCenterID = item.FWorkCenterID,
            //            FShift = item.FShift,
            //            FID = Guid.NewGuid().ToString(),
            //            FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index, //任务计划单号
            //            FMOInterID = input.FMOInterID, //任务单号
            //            FMOBillNo = input.FMOBillNo, //任务单号
            //            FBiller = AbpSession.UserId.ToString(), //当前登录用户
            //            FSrcID = fsrcId,
            //            FPlanAuxQty = item.FPlanAuxQty,
            //            FBillTime = DateTime.Parse(DateTime.Now.ToString("yyyy-M-d HH:mm:ss")),
            //            FDate = item.FDate.Date,
            //            FWorkCenterName = input.FWorkCenterName
            //        };

            //        var entity = Repository.Insert(insertUpdateObj);
            //    }
            //}

            ////更新任务计划单的总排产数
            //await InsertOrUpdateICMOSchedul(new ICMODailyCreatedtEventData
            //{
            //    FSrcID = fsrcId,
            //    FMOBillNo = input.FMOBillNo,
            //    FMOInterID = input.FMOInterID,
            //    FBiller = AbpSession.UserId.ToString(),
            //    FPlanAuxQty = totoalPlan
            //});
            //return null;

            #endregion
            var icmo = MRepository.GetAll().SingleOrDefault(p => p.任务单号 == input.FMOBillNo);
            if (icmo != null)
            {
                var schedule = GetOrCreateSchedule(icmo);
               
                foreach (var dailyItem in input.Dailies)
                {

                    var entity = schedule.Dailies.SingleOrDefault(p =>
                        p.FDate == dailyItem.FDate && p.FMachineID == dailyItem.FMachineID &&
                        p.FShift == dailyItem.FShift && p.FOperID == dailyItem.FOperID);

                    var index = (schedule.Dailies.Count + 1).ToString("000");

                    if (entity == null)
                    {
                        entity = new Entities.ICMODaily
                        {
                            FMachineID = dailyItem.FMachineID,
                            FWorkCenterID = dailyItem.FWorkCenterID,
                            FShift = dailyItem.FShift,
                            FID = Guid.NewGuid().ToString(),
                            FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index, //任务计划单号
                            FMOInterID = icmo.FMOInterID, //任务单ID
                            FMOBillNo = icmo.任务单号, //任务单号
                            FBiller = AbpSession.UserId.ToString(), //当前登录用户
                            FSrcID = schedule.FID,
                            FPlanAuxQty = dailyItem.FPlanAuxQty,
                            FBillTime = DateTime.Now,
                            FDate = dailyItem.FDate.Date,
                            FOperID = dailyItem.FOperID,
                            FWorkCenterName = icmo.车间
                        };
                        //插入新的日计划单
                        schedule.Dailies.Add(entity);
                    }
                    else
                    {
                        schedule.FPlanAuxQty -= entity.FPlanAuxQty;
                        entity.FPlanAuxQty = dailyItem.FPlanAuxQty;
                        schedule.FPlanAuxQty += entity.FPlanAuxQty;
                    }
                }

                //更新计划单
                SRepository.Update(schedule);
                return schedule.Dailies.Count;
            }
            else
            {
                throw new AbpValidationException(string.Format("任务单：{0}不存在",input.FMOBillNo));
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

        public async Task<Array> GetDialyQtyListByFMOInterID(ICMODailyGetAllInput input)
        {
            var query = Repository.GetAll().Where(p => p.FMOInterID == input.FMOInterID);

            var context = Repository.GetDbContext() as ProductionPlanMySqlDbContext;

            var querygroup = from daily in context.ICMODaily
                join disp in context.ICMODispBill on daily.FID equals disp.FSrcID into g1
                from a1 in g1.DefaultIfEmpty()
                select a1;

            var linq = from a in query
                select new {a.FDate, a.FMachineID, a.FPlanAuxQty, a.FCommitAuxQty};

            var data = await linq.ToListAsync();

            var data2 = await querygroup.ToListAsync();

            return data2.ToArray();
        }

        /// <summary>
        ///     任务排产主界面点击“排产”后调用接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<VW_Group_ICMODailyDto>> GetDailyListByFMOInterID(ICMODailyGetAllInput input)
        {
            try
            {
                var query = GrRepository.GetAll().Where(p => p.FMOInterID == input.FMOInterID);

                var data = await query.ToListAsync();

                return data.MapTo<List<VW_Group_ICMODailyDto>>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<ICMOSchedule>> ImportDaily(List<ICMODailyCreatDto> input)
        {
            var fmobillno = "";
            var fmointerid = -1;
            ICMOSchedule schedule = null;
            var result = new List<ICMOSchedule>();
            //遍历导入数据
            foreach (var inputItem in input)
                if (inputItem != null && fmobillno != inputItem.FMOBillNo)
                {
                    fmobillno = inputItem.FMOBillNo;
                   await Create(inputItem);
                    //var icmo = MRepository.GetAll().SingleOrDefault(p => p.任务单号 == fmobillno);
                    //if (icmo != null)
                    //{
                    //    schedule = GetOrCreateSchedule(icmo);

                    //    if (schedule == null)
                    //    {
                    //        schedule = new ICMOSchedule
                    //        {
                    //            FID = Guid.NewGuid().ToString(),
                    //            FBillTime = DateTime.Now,
                    //            FBillNo = "SC-" + fmobillno,
                    //            FBiller = AbpSession.UserId.ToString(),
                    //            FPlanAuxQty = icmo.计划生产数量,
                    //            FItemID = icmo.产品编码,
                    //            FItemModel = icmo.规格型号,
                    //            FItemName = icmo.产品名称,
                    //            FPlanBeginDate = icmo.计划开工日期,
                    //            FPlanFinishDate = icmo.计划完工日期
                    //            ,Dailies = new List<Entities.ICMODaily>()
                    //        };
                    //        //插入新的计划单
                    //        schedule = SRepository.Insert(schedule);
                    //    }

                    //    foreach (var dailyItem in inputItem.Dailies)
                    //    {

                    //        var entity = schedule.Dailies.SingleOrDefault(p =>
                    //            p.FDate == dailyItem.FDate && p.FMachineID == dailyItem.FMachineID &&
                    //            p.FShift == dailyItem.FShift && p.FOperID == dailyItem.FOperID);

                    //        var index = (schedule.Dailies.Count + 1).ToString("000");

                    //        if (entity == null)
                    //        {
                    //            entity = new Entities.ICMODaily
                    //            {
                    //                FMachineID = dailyItem.FMachineID,
                    //                FWorkCenterID = dailyItem.FWorkCenterID,
                    //                FShift = dailyItem.FShift,
                    //                FID = Guid.NewGuid().ToString(),
                    //                FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index, //任务计划单号
                    //                FMOInterID = icmo.FMOInterID, //任务单ID
                    //                FMOBillNo = icmo.任务单号, //任务单号
                    //                FBiller = AbpSession.UserId.ToString(), //当前登录用户
                    //                FSrcID = schedule.FID,
                    //                FPlanAuxQty = dailyItem.FPlanAuxQty,
                    //                FBillTime = DateTime.Now,
                    //                FDate = dailyItem.FDate.Date,
                    //                FWorkCenterName = icmo.车间
                    //            };
                    //            //插入新的日计划单
                    //            schedule.Dailies.Add(entity);
                    //        }
                    //        else
                    //        {
                    //            schedule.FPlanAuxQty -= entity.FPlanAuxQty;
                    //            entity.FPlanAuxQty = dailyItem.FPlanAuxQty;
                    //            schedule.FPlanAuxQty += entity.FPlanAuxQty;
                    //        }
                    //    }

                    //    //更新计划单
                    //    SRepository.Update(schedule);
                    //}
                    //else
                    //{
                    //    throw new AbpException($"任务单号：{0} 不存在" + fmobillno);
                    //}
                }

            return null;
        }

        /// <summary>
        /// 获取或新建任务计划单，如果任务单号已生成计划单则获取，否则新建，并返回计划单
        /// </summary>
        /// <param name="icmo">任务单信息</param>
        /// <returns></returns>
        [UnitOfWork]
        protected  ICMOSchedule GetOrCreateSchedule(Entities.VW_MOBillList icmo)
        {
           var schedule = SRepository.GetAll().Include(p => p.Dailies).SingleOrDefault(p => p.FMOBillNo == icmo.任务单号);

           if (schedule == null)
           {
               schedule = new ICMOSchedule
               {
                   FID = Guid.NewGuid().ToString(),
                   FBillTime = DateTime.Now,
                   FBillNo = "SC-" + icmo.任务单号,
                   FBiller = AbpSession.UserId.ToString(),
                   FPlanAuxQty = icmo.计划生产数量,
                   FItemID = icmo.产品编码,
                   FItemModel = icmo.规格型号,
                   FItemName = icmo.产品名称,
                   FPlanBeginDate = icmo.计划开工日期,
                   FPlanFinishDate = icmo.计划完工日期
                   ,
                   Dailies = new List<Entities.ICMODaily>(),
                   FMOBillNo = icmo.任务单号,
                   FMOInterID = icmo.FMOInterID
               };
               //插入新的计划单
               schedule = SRepository.Insert(schedule);
               UnitOfWorkManager.Current.SaveChanges();
           }

           return schedule;
        }
    }
}