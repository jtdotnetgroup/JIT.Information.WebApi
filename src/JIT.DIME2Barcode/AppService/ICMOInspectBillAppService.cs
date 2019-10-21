﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.ICMOInspectBill.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICQualityRpt.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMOInspectBillList.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JIT.DIME2Barcode.AppService
{
    [AbpAuthorize(ProductionPlanPermissionsNames.TouchPadDispatchedWork,ProductionPlanPermissionsNames.TouchPadIPQC)]
    public class ICMOInspectBillAppService : BaseAppService
    { 
        public async Task<PagedResultDto<VW_ICMOInspectBillListDto>> GetAll(VW_ICMOInspectBillListGetAllInput input)
        {
            var query = JIT_VW_ICMOInspectBillList.GetAll().OrderBy(p => p.派工单号);
            var count = await query.CountAsync();

            var data = await query.PageBy(input).ToListAsync();

            var list = data.MapTo<List<VW_ICMOInspectBillListDto>>();

            return new PagedResultDto<VW_ICMOInspectBillListDto>(count, list);
        }

        /// <summary>
        /// 质检汇报明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICMODispBillDetaileds> ICMOInspectBillDetailed(ICMODispBillDetailedInput input)
        {
            ICMODispBillDetaileds icmoDispBillDetaileds = new ICMODispBillDetaileds();
            // 
            icmoDispBillDetaileds.IcmoInspectBill =
                await JIT_ICMOInspectBill.GetAll().FirstOrDefaultAsync(f =>
                    f.FID == input.FID  && f.FOperID == input.FOperID) ??
                new ICMOInspectBill();
            // 
            //var tmp = JIT_TB_BadItemRelation.GetAll()
            //    .Join(JIT_ICQualityRpt.GetAll(),c=>c.FID, p => p.FItemID, (c, p) => new {p.FID,p.FAuxQty,p.FItemID,p.FNote,p.FRemark,p.ICMOInspectBillID,c.FName,c.FDeleted})
            //    .Where(w => (w.ICMOInspectBillID == input.FID || w.FDeleted == true)).Distinct().ToList();
            //icmoDispBillDetaileds.IcQualityRptsList =  new List<ICQualityRptDto>();
            //foreach (var item in tmp)
            //{
            //    var t = new ICQualityRptDto(){ FID = item.FID,FAuxQty = item.FAuxQty,FItemID = item.FItemID,FName = item.FName,FNote = item.FNote,FRemark = item.FRemark,ICMOInspectBillID = input.FID };
            //    icmoDispBillDetaileds.IcQualityRptsList.Add(t);
            //}
            //

            icmoDispBillDetaileds.IcQualityRptsList =
                JIT_ICQualityRpt.GetAll().Where(w => w.ICMOInspectBillID == input.FID).ToList();
            return icmoDispBillDetaileds;
        }

        /// <summary>
        /// 质检汇报明细保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> ICMODispBillSave(ICMODispBillDetaileds input)
        {
            try
            {
                // 查询回之前的
                var query = await JIT_ICMOInspectBill.GetAll().Where(w => w.FID == input.IcmoInspectBill.FID)
                                .FirstOrDefaultAsync() ?? new ICMOInspectBill();
                query.FID = input.IcmoInspectBill.FID;
                query.FWorker = AbpSession.UserId.ToString();
                query.FNote = input.IcmoInspectBill.FNote;
                query.FOperID = input.IcmoInspectBill.FOperID;
                //query.FBillNo = input.IcmoInspectBill.FBillNo;
                query.FStatus = input.IcmoInspectBill.FAuxQty == input.IcmoInspectBill.FCheckAuxQty ? 1 : 0;
                query.FTranType = 0;
                query.FAuxQty = input.IcmoInspectBill.FAuxQty;
                query.FCheckAuxQty = input.IcmoInspectBill.FCheckAuxQty;
                query.FPassAuxQty = input.IcmoInspectBill.FPassAuxQty;
                query.FFailAuxQty = input.IcmoInspectBill.FFailAuxQty;
                query.FFailAuxQtyP = input.IcmoInspectBill.FFailAuxQtyP;
                query.FFailAuxQtyM = input.IcmoInspectBill.FFailAuxQtyM;
                query.FPassAuxQtyP = input.IcmoInspectBill.FPassAuxQtyP;
                query.FPassAuxQtyM = input.IcmoInspectBill.FPassAuxQtyM;
                query.FInspector = AbpSession.UserId.ToString();
                query.FYSQty = query.FPassAuxQty < input.IcmoInspectBill.FYSQty
                    ? Convert.ToDecimal(query.FPassAuxQty)
                    : Convert.ToDecimal(query.FPassAuxQty % input.IcmoInspectBill.FYSQty);
                query.FInspectTime = DateTime.Now;

                // 更新质检单
                await JIT_ICMOInspectBill.UpdateAsync(query);

                // 删除之前所有不良项目
                var entity = JIT_ICQualityRpt.GetAll().Where(w => w.FID == query.FID);
                foreach (var item in entity)
                {
                    await JIT_ICQualityRpt.DeleteAsync(item);
                }

                // 重新添加所有不良项目
                foreach (var item in input.IcQualityRptsList.Where(w => w.FAuxQty > 0))
                {
                    item.ICMOInspectBillID = query.FID; // 关联主表
                    item.FID = Guid.NewGuid().ToString();
                    await JIT_ICQualityRpt.InsertAsync(item);
                }

                // 所有汇报
                var ICMODispBillList =
                    await JIT_ICMOInspectBill.GetAll().Where(w => w.ICMODispBillID == query.ICMODispBillID).ToListAsync();
                decimal FCheckAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FCheckAuxQty));
                decimal FAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FAuxQty));
                decimal FFailAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FFailAuxQty));
                decimal FPassAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FPassAuxQty));


                // 更新主表
                var query1 = await JIT_ICMODispBill.GetAll().Where(w => w.FID == query.ICMODispBillID)
                    .FirstOrDefaultAsync();

                query1.FFInspectAuxQty = FCheckAuxQty;
                query1.FFinishAuxQty = FAuxQty;
                query1.FFailAuxQty = FFailAuxQty;
                query1.FPassAuxQty = FPassAuxQty;
                
                await JIT_ICMODispBill.UpdateAsync(query1);

                // 没有异常返回 true
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                // 异常返回 false
                return false;
            }
        }
        /// <summary>
        ///  删除质检单
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string FID)
        {
            try
            {
                var result = await JIT_ICMOInspectBill.GetAll().Where(w => w.FID == FID).FirstOrDefaultAsync();
                await JIT_ICMOInspectBill.DeleteAsync(result);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw ;
            }
        }

        /// <summary>
        /// 根据任务单号ID查询回所有检验单号
        /// </summary>
        /// <param name="ICMODispBillID"></param>
        /// <returns></returns>
        public async Task<List<Entities.ICMOInspectBill>> GetList(string ICMODispBillID)
        {
            return await JIT_ICMOInspectBill.GetAll().Where(w => w.ICMODispBillID == ICMODispBillID).ToListAsync();
        }

        /// <summary>
        /// 生成质检单
        /// </summary>
        /// <param name="ICMODispBillID">任务单号ID</param>
        /// <param name="FAuxQty">汇报数</param>
        public bool Create(string ICMODispBillID, decimal FAuxQty,string BatchNum)
        {
            try
            {
                var result = JIT_VW_MODispBillList.GetAll().Where(w => w.FID == ICMODispBillID).FirstOrDefault() ??
                             new VW_MODispBillList();
                ICMOInspectBill icm = new ICMOInspectBill()
                {

                    FID = Guid.NewGuid().ToString(),
                    FMOInterID = 0,
                    FBillNo = "ICM" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1, 10).ToString(),
                    FTranType = 0,
                    FStatus = 0,
                    FOperID = result.FOperID,
                    FWorkcenterID = result.FWorkcenterID,
                    FMachineID = 0,
                    FAuxQty = FAuxQty,
                    FCheckAuxQty = 0,
                    FPassAuxQty = 0,
                    FFailAuxQty = 0,
                    FFailAuxQtyP = 0,
                    FFailAuxQtyM = 0,
                    FPassAuxQtyP = 0,
                    FPassAuxQtyM = 0,
                    FWorker = AbpSession.UserId.ToString(),
                    FBiller = AbpSession.UserId.ToString(),
                    FBillTime = DateTime.Now,
                    ICMODispBillID = ICMODispBillID,
                    FYSQty = 0,
                    BatchNum = BatchNum
                };
                JIT_ICMOInspectBill.InsertOrUpdate(icm);
                // 所有汇报
                var ICMODispBillList = JIT_ICMOInspectBill.GetAll().Where(w => w.ICMODispBillID == ICMODispBillID).ToList();
                // 
                var query1 = JIT_ICMODispBill.GetAll().Where(w => w.FID == ICMODispBillID).SingleOrDefault();
                query1.FFInspectAuxQty = ICMODispBillList.Sum(s => s.FFailAuxQty) + icm.FCheckAuxQty;
                query1.FFinishAuxQty = ICMODispBillList.Sum(s => s.FAuxQty) + icm.FAuxQty;
                query1.FFailAuxQty = ICMODispBillList.Sum(s => s.FFailAuxQty) + icm.FFailAuxQty;
                query1.FPassAuxQty = ICMODispBillList.Sum(s => s.FPassAuxQty) + icm.FPassAuxQty;
                JIT_ICMODispBill.UpdateAsync(query1);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 更改汇报数
        /// </summary>
        public async Task<bool> UpdateFAuxQty(string FID,decimal FAuxQty, string BatchNum)
        {
            try
            {
                var entity = await JIT_ICMOInspectBill.GetAll().SingleOrDefaultAsync(s => s.FID == FID);
                entity.FAuxQty = FAuxQty;
                entity.BatchNum = BatchNum;
                await  JIT_ICMOInspectBill.UpdateAsync(entity);
                // 
                var icmopispBillList = await JIT_ICMOInspectBill.GetAll().Where(w => w.ICMODispBillID == entity.ICMODispBillID).ToListAsync();
                var query1 = await JIT_ICMODispBill.GetAll().Where(w => w.FID == entity.ICMODispBillID).SingleOrDefaultAsync();
                query1.FFInspectAuxQty = icmopispBillList.Sum(s => s.FFailAuxQty);
                query1.FFinishAuxQty = icmopispBillList.Sum(s => s.FAuxQty);
                query1.FFailAuxQty = icmopispBillList.Sum(s => s.FFailAuxQty);
                query1.FPassAuxQty = icmopispBillList.Sum(s => s.FPassAuxQty);
                await JIT_ICMODispBill.UpdateAsync(query1);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 返回余数
        /// </summary>
        public async Task<PagedResultDto<DIME2Barcode.Entities.VW_YSQty>> GetAllYSQty(
            PagedAndSortedResultRequestDto input)
        {
            var query = JIT_ICMODispBill.GetAll().Join(JIT_ICMOSchedule.GetAll(), Z => Z.FMOInterID, Y => Y.FMOInterID,
                    (Z, Y) => new
                    {
                        Z.FID,
                        Z.FBillNo,
                        Z.FBiller,
                        Z.FDate,
                        Z.FStatus,
                        Z.employee,
                        Y.FItemName,
                        Y.FItemID
                    }).Join(JIT_t_ICItem.GetAll(), Y => Y.FItemID, X => X.FItemID, (X, Y) => new
                {
                    X.FID,
                    X.FBillNo,
                    X.FBiller,
                    X.FDate,
                    X.FStatus,
                    X.employee,
                    X.FItemName,
                    Y.F_102
                })
                .Join(JIT_ICMOInspectBill.GetAll(), A => A.FID, B => B.ICMODispBillID,
                    (A, B) => new
                    {
                        A.FID,
                        A.FBillNo,
                        A.FBiller,
                        A.FDate,
                        FBillNo2 = B.FBillNo,
                        B.BatchNum,
                        B.FYSQty,
                        B.FInspector,
                        B.FInspectTime,
                        A.employee.FName,
                        //
                        A.FStatus,
                        A.FItemName,
                        A.F_102
                    })
                .Join(JIT_Employee.GetAll(), B => B.FInspector.ToInt(), C => C.FUserId, (B, C) => new
                {
                    B.FID,
                    B.FBillNo,
                    B.FBiller,
                    B.FDate,
                    B.FBillNo2,
                    B.BatchNum,
                    FYSQty = B.FYSQty - JIT_RemainderLCLMx.GetAll().Where(w => w.ICMOInspectBillId == B.FID).Sum(s => s.SpelledQty),
                    FInspector = C.FName,
                    B.FInspectTime,
                    B.FName,
                    B.FStatus,
                    B.FItemName,
                    B.F_102
                })
                .Where(A => A.FStatus >= PublicEnum.ICMODispBillState.已检验.EnumToInt() && A.FYSQty > 0);
            var data = await query.PageBy(input).ToListAsync();
            var list = data.MapTo<List<DIME2Barcode.Entities.VW_YSQty>>();
            return new PagedResultDto<DIME2Barcode.Entities.VW_YSQty>(query.Count(), list);
        }
    }
}