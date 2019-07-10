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
using Abp.UI;
using CommonTools;
using JIT.DIME2Barcode.AppService;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.EventsAndEventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JIT.DIME2Barcode.TaskAssignment
{
    /// <summary>
    ///     任务排产接口服务
    /// </summary>
    public class ICMODailyAppService : BaseAppService
    {
        public ICMODailyAppService()
        {
            EventBus = NullEventBus.Instance;
        }

        public IEventBus EventBus { get; set; }

        public string getpermission { get; set; }

        //任务单仓储

        //任务计划单仓储
        public IRepository<ICMOSchedule, string> SRepository { get; set; }
        //工序仓储
        public IRepository<t_SubMessage,int> SUbRepository { get; set; }
        //日计划单视图仓储
        public IRepository<VW_ICMODaily, string> VRepository { get; set; }
        //派工单仓储
        public IRepository<Entities.ICMODispBill,string> DRepository { get; set; }
        public IRepository<Entities.VW_MOBillList, string> MRepository { get; set; }
        public IRepository<VW_ICMODaily_Group_By_Day,string> GRepository { get; set; }
        public IRepository<t_OrganizationUnit> ORepository { get; set; }//组织架构仓储
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
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Get)]
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
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Create)]
        public async Task<int> Create(ICMODailyCreatDto input)
        {
            var icmo = MRepository.GetAll().SingleOrDefault(p => p.任务单号 == input.FMOBillNo);

            if(icmo==null)
            {
                this.EX(-1, $"{input.FMOBillNo} 任务单不存在，请检查");
            }

            var oplist = await SUbRepository.GetAll().ToListAsync();

            var equipmentList =await ERepository.GetAll().Where(p => p.FType == PublicEnum.EquipmentType.设备).ToListAsync();
            var orgs = await ORepository.GetAll().Where(p => p.FWorkshopType).ToListAsync();

            var eqShifts = await EsRepository.GetAll().ToListAsync();

            if (icmo != null)
            {
                var schedule = GetOrCreateSchedule(icmo);

                var org=orgs.SingleOrDefault(o => o.DisplayName==icmo.车间);

                if(org==null)
                {
                    this.EX(-1, $"找不到此车间：{icmo.车间}，请检查组织架构信息");
                }

                foreach (var dailyItem in input.Dailies)
                {
                    var equipments = equipmentList.Where(e => e.FName == dailyItem.FMachineName && e.FWorkCenterID == org.Id);

                    if (equipments.Count() > 1)
                    {
                        this.EX(-1, $"【{org.DisplayName}】车间存在多个【{dailyItem.FMachineName}】设备");
                    }

                    var equipment = equipments.SingleOrDefault(); 

                    if (equipment == null) { this.EX(-1,$"找不设备：{dailyItem.FMachineName} ，请检查设备档案");}

                    var shift = eqShifts.SingleOrDefault(p =>
                        p.FEqiupmentID == equipment.FInterID && p.FShift == dailyItem.FShift);

                    if (shift == null) { this.EX(-1,$"设备：{dailyItem.FMachineName} 的班次信息中不存在 【{dailyItem.FShift}】，请检查设备的班次信息");}

                    var entity = schedule.Dailies.SingleOrDefault(p =>
                        p.FDate == dailyItem.FDate && p.FMachineID == equipment.FInterID &&
                        p.FShift == shift.Id);

                    var op = oplist.SingleOrDefault(p => p.FName == dailyItem.FOperID);

                    var index = (schedule.Dailies.Count + 1).ToString("000");

                    if (entity == null&&dailyItem.FPlanAuxQty>0)
                    {
                        entity = new Entities.ICMODaily
                        {
                            FMachineID = equipment.FInterID,
                            FWorkCenterID = org.Id,
                            FShift = shift.Id,
                            FID = Guid.NewGuid().ToString(),
                            FBillNo = "DA" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + index, //任务计划单号
                            FMOInterID = icmo.FMOInterID, //任务单ID
                            FMOBillNo = icmo.任务单号, //任务单号
                            FBiller = AbpSession.UserId.ToString(), //当前登录用户
                            FSrcID = schedule.FID,
                            FPlanAuxQty = dailyItem.FPlanAuxQty,
                            FBillTime = DateTime.Now,
                            FDate = dailyItem.FDate.Date,
                            FOperID = op!=null?op.FInterID:0,
                            FWorkCenterName = icmo.车间,
                            FWorker = shift.FEmployeeID
                        };
                        //插入新的日计划单
                        schedule.Dailies.Add(entity);
                    }
                    else if(dailyItem.FPlanAuxQty>0)
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
                throw new UserFriendlyException(string.Format("任务单：{0}不存在", input.FMOBillNo));
            }
        }
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Update, ProductionPlanPermissionsNames.TaskPlan_Create)]
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
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Update, ProductionPlanPermissionsNames.TaskPlan_Create)]
        public async Task<Array> GetDialyQtyListByFMOInterID(ICMODailyGetAllInput input)
        {
            var query = Repository.GetAll().Where(p => p.FMOInterID == input.FMOInterID);

            var context = Repository.GetDbContext() as ProductionPlanMySqlDbContext;

            var querygroup = from daily in context.ICMODaily
                             join disp in context.ICMODispBill on daily.FID equals disp.FSrcID into g1
                             from a1 in g1.DefaultIfEmpty()
                             select a1;

            var linq = from a in query
                       select new { a.FDate, a.FMachineID, a.FPlanAuxQty, a.FCommitAuxQty };

            var data = await linq.ToListAsync();

            var data2 = await querygroup.ToListAsync();

            return data2.ToArray();
        }

        /// <summary>
        ///     任务排产主界面点击“排产”后调用接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Get)]
        public async Task<List<VW_Group_ICMODaily>> GetDailyListByFMOInterID(ICMODailyGetAllInput input)
        {
            try
            {
                var query = GrRepository.GetAll().Where(p => p.FMOInterID == input.FMOInterID);

                var data = await query.ToListAsync();

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// 导入EXCEL排产信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Import)]
        public async Task<List<ICMOSchedule>> ImportDaily(List<ICMODailyCreatDto> input)
        {
            var fmobillno = "";
            var fmointerid = -1; 
            var result = new List<ICMOSchedule>();
            //遍历导入数据
            foreach (var inputItem in input)
                if (inputItem != null && fmobillno != inputItem.FMOBillNo)
                {
                    fmobillno = inputItem.FMOBillNo;
                    fmointerid = await Create(inputItem);
                }

            return null;
        }

        /// <summary>
        /// 获取或新建任务计划单，如果任务单号已生成计划单则获取，否则新建，并返回计划单
        /// </summary>
        /// <param name="icmo">任务单信息</param>
        /// <returns></returns>
        [UnitOfWork]

        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Get)]
        protected ICMOSchedule GetOrCreateSchedule(Entities.VW_MOBillList icmo)
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
                    FItemNumber = icmo.产品编码,
                    FItemID = icmo.FItemID,
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

        /// <summary>
        /// 任务派工列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Get)]
        public async Task<PagedResultDto<VW_ICMODaily_Group_By_Day>> GetGroupDailyList(ICMODailyGetAllInput input)
        {
            var query = GRepository.GetAll();

            var count = await query.CountAsync();

            var data = await query.OrderBy(p => p.FDate).PageBy(input).ToListAsync();

            return new PagedResultDto<VW_ICMODaily_Group_By_Day>(count,data);

        }

        [AbpAuthorize(ProductionPlanPermissionsNames.TaskPlan_Get)]
        public async Task<MOBillPlanDetail> GetMOBillPlanDetail(ICMODailyGetAllInput input)
        {
            var context = Repository.GetDbContext() as ProductionPlanMySqlDbContext;
            //按FMOInterID汇总计划排产数和派工数
            var query = from daily in context.ICMODaily
                join disp in context.ICMODispBill on daily.FID equals disp.FSrcID into g1
                from a in g1.DefaultIfEmpty()
                where daily.FMOInterID == input.FMOInterID
                group new {daily.FMOInterID,daily.FPlanAuxQty,a.FCommitAuxQty } by daily.FMOInterID
                into s
                select new MOBillPlanDetail()
                {
                    FMOInterID = s.Key,
                    TotalPlan = s.Sum(item=>item.FPlanAuxQty),
                    TotalCommit = s.Sum(item=>item.FCommitAuxQty)
                };

            var header = await query.SingleOrDefaultAsync();
                   
            if (header == null)
            {
                return new MOBillPlanDetail() { FMOInterID = input.FMOInterID,TotalCommit = 0,TotalPlan = 0};
            }

            //按天汇总排产计划数和派工数
            var details = await (from daily in context.ICMODaily
                join disp in context.ICMODispBill on daily.FID equals disp.FSrcID into g1
                from a in g1.DefaultIfEmpty()
                where daily.FMOInterID==header.FMOInterID
                group new {daily.FDate,daily.FPlanAuxQty,a.FCommitAuxQty} by daily.FDate
                into s
                select new MOBillPlanDay()
                {
                    FDate = s.Key,
                    DayCommit = s.Sum(item => item.FCommitAuxQty),
                    DayPlan = s.Sum(item => item.FPlanAuxQty)
                }).OrderBy(p=>p.FDate).ToListAsync();

            header.Details = details;
               
            return header;
        }
    }
}