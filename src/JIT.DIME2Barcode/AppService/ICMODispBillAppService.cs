using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Generators.Emitters;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMODispBill_By_Date.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 派工单接口服务
    /// </summary>
    public class ICMODispBillAppService : BaseAppService
    {     
        /// <summary>
        /// 任务派工界面数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Get)]
        public async Task<PagedResultDto<ICMODispBillListDto>> GetAll(ICMODispBillGetAllInput input)
        {

            var query = JIT_VW_ICMODispBill_By_Date.GetAll()
                .Where(p => p.FMOBillNo == input.FMOBillNo );

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
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Create)]
        public async Task<ICMODispBillListDto> Create(ICMODispBillCreateInput input)
        {
            try
            {
                // 循环生成修改派工单
                foreach (var itemDetail in input.Details)
                {
                    // 日计划单
                    ICMODaily mDaily = await JIT_ICMODaily.GetAll().SingleOrDefaultAsync(p => p.FID == itemDetail.FID);
                    // 判断是否存在
                    if (mDaily == null)
                    {
                        throw new AbpException("日计划单不存在");
                    }
                    // 查看是否已经存在
                    var entity =
                        JIT_ICMODispBill.GetAll().Where(p => p.FSrcID == itemDetail.FID && p.FID == itemDetail.ICMODispBillId)
                            .SingleOrDefault(p => true) ?? new ICMODispBill();
                    // 判断是增加还是修改
                    if (string.IsNullOrWhiteSpace(entity.FID))
                    {
                        /*
                         * 派工单不存在，插入新派工单
                         */
                        entity.FID = Guid.NewGuid().ToString();
                        entity.FSrcID = itemDetail.FID;
                        entity.FBiller = AbpSession.UserId.ToString();
                        entity.FDate = DateTime.Now.Date;
                        entity.FBillNo = "DI" + DateTime.Now.ToString("yyyyMMddHHmmss") + entity.FID.Right(4);
                        entity.FBillTime = DateTime.Now;
                    }
                    else
                    {
                        /*
                         * 派工单已存在，更新派工单信息
                         */
                        mDaily.FCommitAuxQty -= entity.FCommitAuxQty;

                        entity.FDate = DateTime.Now;
                    }
                    // 修改派工
                    entity.FCommitAuxQty = itemDetail.FCommitAuxQty;
                    entity.FWorker = itemDetail.FWorker;
                    entity.FShift = itemDetail.FShift;
                    entity.FMachineID = itemDetail.FMachineID;
                    entity.FMOBillNo = itemDetail.FMOBillNo;
                    entity.FMOInterID = itemDetail.FMOInterID;
                    entity.FWorkCenterID = mDaily.FWorkCenterID;
                    // 
                    itemDetail.ICMODispBillId = string.IsNullOrWhiteSpace(itemDetail.ICMODispBillId)
                        ? "0"
                        : itemDetail.ICMODispBillId;
                    // 判断是增加还是修改
                    if (itemDetail.ICMODispBillId.Equals("0"))
                    {
                        await JIT_ICMODispBill.InsertAsync(entity);
                    }
                    else
                    {
                        await JIT_ICMODispBill.UpdateAsync(entity);
                    } 
                    // 修改日计划
                    mDaily.FCommitAuxQty += entity.FCommitAuxQty;
                    mDaily.FWorker = itemDetail.FWorker;
                    await JIT_ICMODaily.UpdateAsync(mDaily);
                }
                return null;
            }
            catch (Exception e)
            {
                EX(-1,"系统提示",e.Message);
                throw; 
            }
        }
        ///// <summary>
        ///// 日计划单生成或更新派工单
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Create)]
        //public async Task<ICMODispBillListDto> Create(ICMODispBillCreateInput input)
        //{
        //    decimal? totalCommitQty = 0;

        //    try
        //    {

        //        foreach (var dispBillI in input.Details)
        //        {
        //            var dailyFid = dispBillI.FID;
        //            var dispBillList = await JIT_ICMODispBill.GetAll().Where(p => p.FSrcID == dailyFid).ToListAsync();
        //            var icmodaily = await JIT_ICMODaily.GetAll().SingleOrDefaultAsync(p => p.FID == dailyFid);

        //            if (icmodaily == null)
        //            {
        //                throw new AbpException("日计划单不存在");
        //            }

        //            var entity = dispBillList.SingleOrDefault(p => p.FSrcID == dispBillI.FID);

        //            if (entity == null)
        //            {
        //                /*
        //                 *派工单不存在，插入新派工单
        //                 */
        //                string fid = Guid.NewGuid().ToString();
        //                entity = new ICMODispBill()
        //                {
        //                    FID = fid,
        //                    FSrcID = dailyFid,
        //                    FWorker = dispBillI.FWorker,
        //                    FWorkCenterID = icmodaily.FWorkCenterID,
        //                    FMachineID = dispBillI.FMachineID,
        //                    FMOBillNo = dispBillI.FMOBillNo,
        //                    FMOInterID = dispBillI.FMOInterID,
        //                    FCommitAuxQty = dispBillI.FCommitAuxQty,
        //                    FBiller = AbpSession.UserId.ToString(),
        //                    FDate = DateTime.Now.Date,
        //                    FShift = dispBillI.FShift,
        //                    FBillNo = "DI" + DateTime.Now.ToString("yyyyMMddHHmmss") +fid.Right(4) ,
        //                    FBillTime = DateTime.Now

        //                };

        //                //totalCommitQty += dispBillI.FCommitAuxQty;
        //                icmodaily.FCommitAuxQty += dispBillI.FCommitAuxQty;

        //                await JIT_ICMODispBill.InsertAsync(entity);
        //            }
        //            else
        //            {
        //                /*
        //             *派工单已存在，更新派工单信息
        //             */
        //                icmodaily.FCommitAuxQty -= entity.FCommitAuxQty;

        //                entity.FWorkCenterID = icmodaily.FWorkCenterID;
        //                entity.FWorker = dispBillI.FWorker;
        //                entity.FCommitAuxQty = dispBillI.FCommitAuxQty;
        //                entity.FMachineID = dispBillI.FMachineID;
        //                entity.FDate = DateTime.Now;
        //                entity.FShift = dispBillI.FShift;
        //                entity.FMachineID = dispBillI.FMachineID;
        //                entity.FMOBillNo = dispBillI.FMOBillNo;
        //                entity.FMOInterID = dispBillI.FMOInterID;
        //                await JIT_ICMODispBill.UpdateAsync(entity);

        //                icmodaily.FCommitAuxQty += entity.FCommitAuxQty;
        //            }

        //            icmodaily.FWorker = dispBillI.FWorker;

        //            await JIT_ICMODaily.UpdateAsync(icmodaily);
        //        }

        //        //foreach (var dispBillI in input.Details)
        //        //{
        //        //    

        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}

        /// <summary>
        /// 任务派工主表列表接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Get)]
        public async Task<PagedResultDto<VW_ICMODispBill_By_Date>> GetDailyGroup(
            VW_ICMODispBill_By_DateGetAllInput input)
        {
            var query = JIT_VW_ICMODispBill_By_Date.GetAll();

            var count = await query.CountAsync();

            var data = await query.OrderBy(p => p.日期).PageBy(input).ToListAsync();

            return new PagedResultDto<VW_ICMODispBill_By_Date>(count, data);
        }
         
        public async Task<bool> UpdateFFinishAuxQty(ICMODispBillUpdateFFinishAuxQtyInput input)
        {
            try
            {
                var entity = await JIT_ICMODispBill.GetAll().SingleOrDefaultAsync(p => p.FID == input.FID);
                if (entity != null)
                {
                    entity.FStatus = 2;
                    entity.FFinishAuxQty = input.FFinishAuxQty;
                    await JIT_ICMODispBill.UpdateAsync(entity);
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
                var entity = await JIT_ICMODispBill.GetAll().Include(p=>p.employee)
                    .SingleOrDefaultAsync(p => p.FID == input.FID && p.employee.FUserId == AbpSession.UserId);
                if (entity != null)
                {
                    entity.FStatus = PublicEnum.ICMODispBillState.待汇报.EnumToInt();
                    entity.FStartTime = DateTime.Now;
                    await JIT_ICMODispBill.UpdateAsync(entity);
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
            var query = JIT_ICMODispBill.GetAll()
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
        /// <summary>
        ///日计划任务明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<VW_DispatchBill_List>> Post_GetDailyDispatchList(ICMODispBill_Daily_GetAllListInput input)
        {
            var query = JIT_VW_DispatchBill_List.GetAll();

            query = from a in query
                    where input.FMOBillNos.Contains(a.FMOBillNo) && input.DatelList.Contains(a.FDate)
                    select a;

            var count = await query.CountAsync();

            var data = await query.OrderBy(a=>a.FDate).ToListAsync();

            return new PagedResultDto<VW_DispatchBill_List>(count, data);
        }

        /// <summary>
        /// 更改派工单状态
        /// </summary>
        public async Task<bool> UpdateFStatus(string FID,int FStates)
        {
            try
            {
                var query = await JIT_ICMODispBill.GetAll().Where(s => s.FID == FID).SingleOrDefaultAsync();
                query.FStatus = FStates;
                await JIT_ICMODispBill.UpdateAsync(query);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            } 
        }
        /// <summary>
        /// 关闭任务单
        /// </summary>
        [HttpDelete]
        public async Task<bool> CloseOrder(string id)
        {
            try
            {
                var result = await JIT_ICMODispBill.GetAll().Where(w => id.Contains(w.FID)).ToListAsync();
                foreach (var item in result)
                {
                    item.FStatus = -1;
                    item.FClosed = true;
                    await JIT_ICMODispBill.UpdateAsync(item);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}