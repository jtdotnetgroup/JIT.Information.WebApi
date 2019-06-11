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
using JIT.DIME2Barcode.TaskAssignment.ICMOInspectBill.Dtos;
using JIT.DIME2Barcode.TaskAssignment.ICQualityRpt.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMOInspectBillList.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JIT.DIME2Barcode.AppService
{
    public class ICMOInspectBillAppService : ApplicationService
    {
        public IRepository<ICMOInspectBill, string> Repository { get; set; }
        public IRepository<VW_ICMOInspectBillList, string> VRepository { get; set; }
        public IRepository<DIME2Barcode.Entities.ICQualityRpt, string> IcRepository { get; set; }
        public IRepository<ICMODispBill, string> icmodispbillRepository { get; set; }
        public IRepository<DIME2Barcode.Entities.TB_BadItemRelation, int> tbRepository { get; set; }

        public IRepository<DIME2Barcode.Entities.VW_MODispBillList, string> vm_MODispBillList { get; set; }

        public async Task<PagedResultDto<VW_ICMOInspectBillListDto>> GetAll(VW_ICMOInspectBillListGetAllInput input)
        {
            var query = VRepository.GetAll().OrderBy(p => p.派工单号);
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
                await Repository.GetAll().FirstOrDefaultAsync(f =>
                    f.FID == input.FID  && f.FOperID == input.FOperID) ??
                new ICMOInspectBill();
            // 
            //var tmp = tbRepository.GetAll()
            //    .Join(IcRepository.GetAll(),c=>c.FID, p => p.FItemID, (c, p) => new {p.FID,p.FAuxQty,p.FItemID,p.FNote,p.FRemark,p.ICMOInspectBillID,c.FName,c.FDeleted})
            //    .Where(w => (w.ICMOInspectBillID == input.FID || w.FDeleted == true)).Distinct().ToList();
            //icmoDispBillDetaileds.IcQualityRptsList =  new List<ICQualityRptDto>();
            //foreach (var item in tmp)
            //{
            //    var t = new ICQualityRptDto(){ FID = item.FID,FAuxQty = item.FAuxQty,FItemID = item.FItemID,FName = item.FName,FNote = item.FNote,FRemark = item.FRemark,ICMOInspectBillID = input.FID };
            //    icmoDispBillDetaileds.IcQualityRptsList.Add(t);
            //}
            //

            icmoDispBillDetaileds.IcQualityRptsList =
                IcRepository.GetAll().Where(w => w.ICMOInspectBillID == input.FID).ToList();
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
                var query = await Repository.GetAll().Where(w => w.FID == input.IcmoInspectBill.FID)
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
                await Repository.UpdateAsync(query);

                // 删除之前所有不良项目
                var entity = IcRepository.GetAll().Where(w => w.FID == query.FID);
                foreach (var item in entity)
                {
                    await IcRepository.DeleteAsync(item);
                }

                // 重新添加所有不良项目
                foreach (var item in input.IcQualityRptsList.Where(w => w.FAuxQty > 0))
                {
                    item.ICMOInspectBillID = query.FID; // 关联主表
                    item.FID = Guid.NewGuid().ToString();
                    await IcRepository.InsertAsync(item);
                }

                // 所有汇报
                var ICMODispBillList =
                    await Repository.GetAll().Where(w => w.ICMODispBillID == query.ICMODispBillID).ToListAsync();
                decimal FCheckAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FCheckAuxQty));
                decimal FAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FAuxQty));
                decimal FFailAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FFailAuxQty));
                decimal FPassAuxQty =
                    Convert.ToDecimal(ICMODispBillList.Sum(s => s.FPassAuxQty));


                // 更新主表
                var query1 = await icmodispbillRepository.GetAll().Where(w => w.FID == query.ICMODispBillID)
                    .FirstOrDefaultAsync();

                query1.FFInspectAuxQty = FCheckAuxQty;
                query1.FFinishAuxQty = FAuxQty;
                query1.FFailAuxQty = FFailAuxQty;
                query1.FPassAuxQty = FPassAuxQty;
                
                await icmodispbillRepository.UpdateAsync(query1);

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
                var result = await Repository.GetAll().Where(w => w.FID == FID).FirstOrDefaultAsync();
                Repository.DeleteAsync(result);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 根据任务单号ID查询回所有检验单号
        /// </summary>
        /// <param name="ICMODispBillID"></param>
        /// <returns></returns>
        public async Task<List<Entities.ICMOInspectBill>> GetList(string ICMODispBillID)
        {
            return await Repository.GetAll().Where(w => w.ICMODispBillID == ICMODispBillID).ToListAsync();
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
                var result = vm_MODispBillList.GetAll().Where(w => w.FID == ICMODispBillID).FirstOrDefault() ??
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
                Repository.InsertOrUpdate(icm);
                // 所有汇报
                var ICMODispBillList = Repository.GetAll().Where(w => w.ICMODispBillID == ICMODispBillID).ToList();
                // 
                var query1 = icmodispbillRepository.GetAll().Where(w => w.FID == ICMODispBillID).SingleOrDefault();
                query1.FFInspectAuxQty = ICMODispBillList.Sum(s => s.FFailAuxQty) + icm.FCheckAuxQty;
                query1.FFinishAuxQty = ICMODispBillList.Sum(s => s.FAuxQty) + icm.FAuxQty;
                query1.FFailAuxQty = ICMODispBillList.Sum(s => s.FFailAuxQty) + icm.FFailAuxQty;
                query1.FPassAuxQty = ICMODispBillList.Sum(s => s.FPassAuxQty) + icm.FPassAuxQty;
                icmodispbillRepository.UpdateAsync(query1);
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
                var entity = await Repository.GetAll().SingleOrDefaultAsync(s => s.FID == FID);
                entity.FAuxQty = FAuxQty;
                entity.BatchNum = BatchNum;
                await  Repository.UpdateAsync(entity);
                // 
                var icmopispBillList = await Repository.GetAll().Where(w => w.ICMODispBillID == entity.ICMODispBillID).ToListAsync();
                var query1 = await icmodispbillRepository.GetAll().Where(w => w.FID == entity.ICMODispBillID).SingleOrDefaultAsync();
                query1.FFInspectAuxQty = icmopispBillList.Sum(s => s.FFailAuxQty);
                query1.FFinishAuxQty = icmopispBillList.Sum(s => s.FAuxQty);
                query1.FFailAuxQty = icmopispBillList.Sum(s => s.FFailAuxQty);
                query1.FPassAuxQty = icmopispBillList.Sum(s => s.FPassAuxQty);
                await icmodispbillRepository.UpdateAsync(query1);

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