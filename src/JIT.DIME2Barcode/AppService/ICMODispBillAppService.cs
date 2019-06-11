using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Generators.Emitters;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMODispBill_By_Date.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 派工单接口服务
    /// </summary>
    public class ICMODispBillAppService : ApplicationService
    {
        public IRepository<VW_ICMODispBill_By_Date, string> VRepository { get; set; }
        public IRepository<ICMODaily, string> DRepository { get; set; }
        public IRepository<ICMODispBill, string> Repository { get; set; }
        public IRepository<VW_DispatchBill_List, string> LRepository { get; set; }
        //员工仓储
        //public IRepository<Employee, int> ERepository { get; set; }

        /// <summary>
        /// 任务派工界面数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ICMODispBillListDto>> GetAll(ICMODispBillGetAllInput input)
        {

            var query = VRepository.GetAll()
                .Where(p => p.FMOBillNo == input.FMOBillNo && p.FMOInterID == input.FMOInterID);

            query = input.FDate == null ? query : query.Where(p => p.日期 == input.FDate);

            var count = await query.CountAsync();
            try
            {
                var data = await query.ToListAsync();
                var list = data.MapTo<List<ICMODispBillListDto>>();

                return new PagedResultDto<ICMODispBillListDto>(count, list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 日计划单生成或更新派工单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ICMODispBillListDto> Create(ICMODispBillCreateInput input)
        {
            decimal? totalCommitQty = 0;

            try
            {

                foreach (var dispBillI in input.Details)
                {
                    var dailyFid = dispBillI.FID;
                    var dispBillList = await Repository.GetAll().Where(p => p.FSrcID == dailyFid).ToListAsync();
                    var icmodaily = await DRepository.GetAll().SingleOrDefaultAsync(p => p.FID == dailyFid);

                    if (icmodaily == null)
                    {
                        throw new AbpException("日计划单不存在");
                    }

                    var entity = dispBillList.SingleOrDefault(p => p.FSrcID == dispBillI.FID);

                    if (entity == null)
                    {
                        /*
                         *派工单不存在，插入新派工单
                         */
                        entity = new ICMODispBill()
                        {
                            FID = Guid.NewGuid().ToString(),
                            FSrcID = dailyFid,
                            FWorker = dispBillI.FWorker,
                            FWorkCenterID = icmodaily.FWorkCenterID,
                            FMachineID = dispBillI.FMachineID,
                            FMOBillNo = dispBillI.FMOBillNo,
                            FMOInterID = dispBillI.FMOInterID,
                            FCommitAuxQty = dispBillI.FCommitAuxQty,
                            FBiller = AbpSession.UserId.ToString(),
                            FDate = DateTime.Now.Date,
                            FShift = dispBillI.FShift,
                            FBillNo = "DI" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1, 10).ToString(),
                            FBillTime = DateTime.Now

                        };

                        //totalCommitQty += dispBillI.FCommitAuxQty;
                        icmodaily.FCommitAuxQty += dispBillI.FCommitAuxQty;

                        await Repository.InsertAsync(entity);
                    }
                    else
                    {
                        /*
                     *派工单已存在，更新派工单信息
                     */
                        icmodaily.FCommitAuxQty -= entity.FCommitAuxQty;

                        entity.FWorkCenterID = icmodaily.FWorkCenterID;
                        entity.FWorker = dispBillI.FWorker;
                        entity.FCommitAuxQty = dispBillI.FCommitAuxQty;
                        entity.FMachineID = dispBillI.FMachineID;
                        entity.FDate = DateTime.Now;
                        entity.FShift = dispBillI.FShift;
                        entity.FMachineID = dispBillI.FMachineID;
                        entity.FMOBillNo = dispBillI.FMOBillNo;
                        entity.FMOInterID = dispBillI.FMOInterID;
                        await Repository.UpdateAsync(entity);

                        icmodaily.FCommitAuxQty += entity.FCommitAuxQty;
                    }

                    icmodaily.FWorker = dispBillI.FWorker;

                    await DRepository.UpdateAsync(icmodaily);
                }

                //foreach (var dispBillI in input.Details)
                //{
                //    

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 任务派工主表列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<VW_ICMODispBill_By_Date>> GetDailyGroup(
            VW_ICMODispBill_By_DateGetAllInput input)
        {
            var query = VRepository.GetAll();

            var count = await query.CountAsync();

            var data = await query.OrderBy(p => p.日期).PageBy(input).ToListAsync();

            return new PagedResultDto<VW_ICMODispBill_By_Date>(count, data);
        }

        public async Task<bool> UpdateFFinishAuxQty(ICMODispBillUpdateFFinishAuxQtyInput input)
        {
            try
            {
                var entity = await Repository.GetAll().SingleOrDefaultAsync(p => p.FID == input.FID);
                if (entity != null)
                {
                    entity.FStatus = 2;
                    entity.FFinishAuxQty = input.FFinishAuxQty;
                    await Repository.UpdateAsync(entity);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 开工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> OpenWork(ICMODispBillOpenWorkInput input)
        {
            try
            {
                var entity = await Repository.GetAll().Include(p=>p.employee)
                    .SingleOrDefaultAsync(p => p.FID == input.FID && p.employee.FUserId == AbpSession.UserId);
                if (entity != null)
                {
                    entity.FStatus = PublicEnum.ICMODispBillState.待汇报.EnumToInt();
                    entity.FStartTime = DateTime.Now;
                    await Repository.UpdateAsync(entity);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


        /// <summary>
        /// 任务派工主表列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Array> GetDailyFsrIdTask(
            ICMODispBillGetAllInputTwo input)
        {
            var query = Repository.GetAll()
                .Where(p => p.FSrcID == input.FSrcID);
            var count = await query.CountAsync();
            try
            {
                var data = await query.ToListAsync();
                var list = from a in data
                           select new
                           {

                               a.FID,
                               a.FSrcID,
                               设备 = a.FMachineID,
                               操作员 = a.FWorker,
                               班次 = a.FShift,
                               派工数量 = a.FCommitAuxQty,
                               派工单号 = a.FBillNo,
                               派单时间 = a.FBillTime,
                               计划员 = a.FBiller,
                               备注 = a.FNote,
                               日期 = a.FDate
                           };

                return list.ToArray();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<PagedResultDto<VW_DispatchBill_List>> GetDailyDispatchList(ICMODispBill_Daily_GetAllListInput input)
        {
            var query = LRepository.GetAll();

            query = from a in query
                    where input.FMOBillNos.Contains(a.FMOBillNo) && input.DatelList.Contains(a.FDate)
                    select a;

            var count = await query.CountAsync();

            var data = await query.ToListAsync();

            return new PagedResultDto<VW_DispatchBill_List>(count, data);
        }

        /// <summary>
        /// 更改派工单状态
        /// </summary>
        public async Task<bool> UpdateFStatus(string FID,int FStates)
        {
            try
            {
                var query = await Repository.GetAll().Where(s => s.FID == FID).SingleOrDefaultAsync();
                query.FStatus = FStates;
                await Repository.UpdateAsync(query);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
           
        }
    }


}