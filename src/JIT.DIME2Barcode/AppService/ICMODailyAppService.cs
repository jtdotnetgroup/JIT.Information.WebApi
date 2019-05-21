using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.EventsAndEventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace JIT.DIME2Barcode.TaskAssignment
{
    /// <summary>
    /// 任务排产接口服务
    /// </summary>
    public class ICMODailyAppService :ApplicationService
    {
        public IEventBus EventBus { get; set; } 

        //任务单仓储
        public IRepository<ICMO> MRepository { get; set; }

        //任务计划单仓储
        public IRepository<ICMOSchedule, string> SRepository { get; set; }
        //日计划单视图仓储
        public IRepository<VW_ICMODaily, string> VRepository { get; set; }
        //日计划单仓储
        public IRepository<Entities.ICMODaily,string> Repository { get; set; }

        public IRepository<VW_Group_ICMODaily, string> GrRepository { get; set; }

        public ICMODailyAppService()
        {
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// 任务派工界面列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<PagedResultDto<VW_ICMODailyDto>> GetAll(ICMODailyGetAllInput input)
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
        /// 生成日计划单接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async Task<VW_ICMODailyDto> Create(ICMODailyCreatDto input)
        {
            var dailyList = await Repository.GetAll().Where(p => p.FMOBillNo == input.FMOBillNo&&p.FMOInterID==input.FMOInterID).ToListAsync();
            var fsrcId = ""; //任务计划单FID
            decimal? totoalPlan = 0;//计划单排产数

            if (dailyList == null || dailyList.Count == 0)
            {
                fsrcId = Guid.NewGuid().ToString();
            }
            else
            {
                fsrcId = dailyList.First().FSrcID;
                totoalPlan += dailyList.Sum(p => p.FPlanAuxQty);
            }
            //插入日计划单
            foreach (var item in input.Dailies)
            {
                var daily = dailyList.SingleOrDefault(p =>
                    p.FShift == item.FShift && p.FMachineID == item.FMachineID &&
                    item.FWorkCenterID == p.FWorkCenterID && item.FDate == p.FDate);

                
                Entities.ICMODaily insertUpdateObj = null;

                if (daily != null)
                {
                    totoalPlan -= daily.FPlanAuxQty;
                    totoalPlan += item.FPlanAuxQty;
                    insertUpdateObj = daily;
                    insertUpdateObj.FPlanAuxQty = item.FPlanAuxQty;
                    Repository.Update(insertUpdateObj);
                }
                else
                {
                    totoalPlan += item.FPlanAuxQty;
                    var index = (input.Dailies.IndexOf(item)+dailyList.Count + 1).ToString("000");
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
                        FBillTime = DateTime.Parse(DateTime.Now.ToString("yyyy-M-d HH:mm:ss")),
                        FDate =item.FDate.Date
                    };

                   var entity=   Repository.Insert(insertUpdateObj);
                }
            }
            //更新任务计划单的总排产数
             await InsertOrUpdateICMOSchedul(new ICMODailyCreatedtEventData
            {
                FSrcID = fsrcId,
                FMOBillNo = input.FMOBillNo,
                FMOInterID = input.FMOInterID,
                FBiller = AbpSession.UserId.ToString(),
                FPlanAuxQty = totoalPlan
            });
            return null;
        }

        protected async Task InsertOrUpdateICMOSchedul(ICMODailyCreatedtEventData eventData)
        {
            var entity = await SRepository.GetAll().SingleOrDefaultAsync(p =>
                p.FID == eventData.FSrcID);

            if (entity == null)
            {
                entity = new ICMOSchedule()
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
                select new {a.FDate,a.FMachineID,a.FPlanAuxQty,a.FCommitAuxQty};

            var data = await linq.ToListAsync();

            var data2 =await querygroup.ToListAsync();

            return data2.ToArray();

        }
        /// <summary>
        /// 任务排产主界面点击“排产”后调用接口
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

        public List<ICMOSchedule> ImportDaily(List<ICMODailyCreatDto> input)
        {
            string fmobillno = "";
            int fmointerid = -1;
            ICMOSchedule schedule = null;
            List<ICMOSchedule> result = new List<ICMOSchedule>();
            //遍历导入数据
            foreach (var inputItem in input)
            {
                if (inputItem != null && fmobillno != inputItem.FMOBillNo)
                {
                    fmobillno = inputItem.FMOBillNo;
                    var icmo = MRepository.GetAll().SingleOrDefault(p => p.FBillNo == fmobillno);
                    if (icmo != null)
                    {
                        schedule = SRepository.GetAll().SingleOrDefault(p => p.FMOBillNo == fmobillno);

                        if (schedule == null)
                        {
                            schedule = new ICMOSchedule()
                            {
                                FID = Guid.NewGuid().ToString(),
                                FBillTime = DateTime.Now,
                                FBillNo = "SC-" + fmobillno,
                                FBiller = AbpSession.UserId.ToString(),
                                FPlanAuxQty = 0
                            };
                            //插入新的计划单
                            schedule = SRepository.Insert(schedule);
                        }

                        foreach (var dailyItem in inputItem.Dailies)
                        {
                            #region 暂时注掉不用




                            //var entity = dailyList.SingleOrDefault(p => p.FDate == dailyItem.FDate && p.FMachineID == dailyItem.FMachineID &&
                            //                                            p.FShift == dailyItem.FShift && p.FOperID == dailyItem.FOperID);

                            //var index = (inputItem.Dailies.IndexOf(dailyItem) + dailyList.Count + 1).ToString("000");

                            //if (entity == null)
                            //{
                            //    entity = new Entities.ICMODaily()
                            //    {
                            //        FMachineID = dailyItem.FMachineID,
                            //        FWorkCenterID = dailyItem.FWorkCenterID,
                            //        FShift = dailyItem.FShift,
                            //        FID = Guid.NewGuid().ToString(),
                            //        FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index,//任务计划单号
                            //        FMOInterID = icmo.FInterID,//任务单号
                            //        FMOBillNo = icmo.FBillNo,//任务单号
                            //        FBiller = this.AbpSession.UserId.ToString(),//当前登录用户
                            //        FSrcID = schedule.FID,
                            //        FPlanAuxQty = dailyItem.FPlanAuxQty,
                            //        FBillTime = DateTime.Now,
                            //        FDate = dailyItem.FDate.Date
                            //    };

                            //}
                            //else
                            //{
                            //    entity.FPlanAuxQty = dailyItem.FPlanAuxQty;
                            //}
                            #endregion

                            var entity = schedule.Dailies.SingleOrDefault(p =>
                                p.FDate == dailyItem.FDate && p.FMachineID == dailyItem.FMachineID &&
                                p.FShift == dailyItem.FShift && p.FOperID == dailyItem.FOperID);

                            var index = (schedule.Dailies.Count + 1).ToString("000");

                            if (entity == null)
                            {
                                entity = new Entities.ICMODaily()
                                {
                                    FMachineID = dailyItem.FMachineID,
                                    FWorkCenterID = dailyItem.FWorkCenterID,
                                    FShift = dailyItem.FShift,
                                    FID = Guid.NewGuid().ToString(),
                                    FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index,//任务计划单号
                                    FMOInterID = icmo.FInterID,//任务单号
                                    FMOBillNo = icmo.FBillNo,//任务单号
                                    FBiller = this.AbpSession.UserId.ToString(),//当前登录用户
                                    FSrcID = schedule.FID,
                                    FPlanAuxQty = dailyItem.FPlanAuxQty,
                                    FBillTime =  DateTime.Parse(DateTime.Now.ToString("yyyy-M-d dd HH:mm:ss")),
                                    FDate = dailyItem.FDate.Date
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
                    }
                    else
                    {
                        throw new AbpException($"任务单号：{0} 不存在" + fmobillno);
                    }

                }
            }

            return null;
        }
    }
}