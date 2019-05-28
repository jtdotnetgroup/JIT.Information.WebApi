using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Generators.Emitters;
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

        //public IRepository<ICMODaily,string> DRepository { get; set; }
        public IRepository<ICMODispBill, string> Repository { get; set; }
        //public IRepository<ICMOSchedule, string> SRepository { get; set; }

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
                var list = data.MapTo(new List<ICMODispBillListDto>());

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
                    var dailyFid = dispBillI.FSrcID;
                    var entity = await Repository.GetAll().SingleOrDefaultAsync(p => p.FSrcID == dailyFid);
                    if (entity == null)
                    {
                        /*
                         *派工单不存在，插入新派工单
                         */
                        entity = new ICMODispBill()
                        {
                            FID = Guid.NewGuid().ToString(),
                            FSrcID = dailyFid,
                            FWorkCenterID = dispBillI.FWorkCenterID,
                            FMachineID = dispBillI.FMachineID,
                            FMOBillNo = dispBillI.FMOBillNo,
                            FMOInterID = dispBillI.FMOInterID,
                            FCommitAuxQty = dispBillI.FCommitAuxQty,
                            FBiller = AbpSession.UserId.ToString(),
                            FDate = DateTime.Now,
                            FBillNo = "DI" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                      new Random().Next(1, 10).ToString()
                        };

                        totalCommitQty += dispBillI.FCommitAuxQty;

                        await Repository.InsertAsync(entity);

                    }
                    else
                    {
                        /*
                         *派工单已存在，更新派工单信息
                         */
                        entity.FWorkCenterID = dispBillI.FWorkCenterID;
                        entity.FWorker = dispBillI.FWorker;
                        entity.FCommitAuxQty = dispBillI.FCommitAuxQty;
                        entity.FMachineID = dispBillI.FMachineID;
                        entity.FDate = DateTime.Now;

                        await Repository.UpdateAsync(entity);

                        totalCommitQty += entity.FCommitAuxQty;
                    }
                }

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
        

    }


}