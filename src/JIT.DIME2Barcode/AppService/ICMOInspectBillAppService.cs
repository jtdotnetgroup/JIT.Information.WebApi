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
    public class ICMOInspectBillAppService:ApplicationService
    {
        public IRepository<ICMOInspectBill,string> Repository { get; set; }
        public IRepository<VW_ICMOInspectBillList,string> VRepository { get; set; }
        public IRepository<DIME2Barcode.Entities.ICQualityRpt, string> IcRepository { get; set; }
        public IRepository<ICMODispBill, string>icmodispbillRepository { get; set; }
        public IRepository<DIME2Barcode.Entities.TB_BadItemRelation, int> tbRepository { get; set; }
        public async Task<PagedResultDto<VW_ICMOInspectBillListDto>> GetAll(VW_ICMOInspectBillListGetAllInput input)
        {
            var query = VRepository.GetAll().OrderBy(p=>p.派工单号);
            var count = await query.CountAsync();

            var data = await query.PageBy(input).ToListAsync();

            var list = data.MapTo<List<VW_ICMOInspectBillListDto>>();

            return new PagedResultDto<VW_ICMOInspectBillListDto>(count,list);
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
                    f.FID == input.FID && f.FBillNo == input.FBillNo && f.FOperID == input.FOperID) ??
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

                var query = await Repository.GetAll().Where(w => w.FID == input.IcmoInspectBill.FID)
                    .FirstOrDefaultAsync()??new ICMOInspectBill();
                input.IcmoInspectBill.Id = string.IsNullOrEmpty(query.FID) ? "0" : "1";
                query.FID = input.IcmoInspectBill.FID;
                query.FWorker = AbpSession.UserId.ToString();
                query.FNote = input.IcmoInspectBill.FNote;
                query.FOperID = input.IcmoInspectBill.FOperID;
                query.FBillNo = input.IcmoInspectBill.FBillNo;
                query.FStatus = 1;
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
                query.FInspectTime = DateTime.Now;
                //
                if (input.IcmoInspectBill.Id == "0")
                {
                    //query.FID= Guid.NewGuid().ToString();
                    await Repository.InsertAsync(query);
                }
                else
                {
                    await Repository.UpdateAsync(query);
                }
                // 
                var entity = IcRepository.GetAll().Where(w => w.FID == input.IcmoInspectBill.FID);
                foreach (var item in entity)
                {
                    await IcRepository.DeleteAsync(item);
                }
                // 
                foreach (var item in input.IcQualityRptsList.Where(w => w.FAuxQty > 0))
                {
                    item.ICMOInspectBillID = query.FID;  // 关联主表
                    item.FID = Guid.NewGuid().ToString();
                    await IcRepository.InsertAsync(item);
                }

                var query1 = await icmodispbillRepository.GetAll().Where(w => w.FID == input.IcmoInspectBill.FID)
                    .FirstOrDefaultAsync();
                query1.FFInspectAuxQty = input.IcmoInspectBill.FCheckAuxQty;
                query1.FFinishAuxQty = input.IcmoInspectBill.FAuxQty;
                query1.FFailAuxQty = input.IcmoInspectBill.FFailAuxQty;
                query1.FPassAuxQty = input.IcmoInspectBill.FPassAuxQty;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
    }
}