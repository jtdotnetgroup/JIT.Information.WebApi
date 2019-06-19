using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.ICException.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{

    /// <summary>
    /// 派工异常记录
    /// </summary>
    public class ICExceptionAppService : BaseAppService
    { 
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ICExceptionDto> Get(ICExceptionInput input)
        {
            var entity = await JIT_ICException.GetAll()
                .SingleOrDefaultAsync(p => p.FID == input.FID && p.FSrcID == input.FSrcID);
            return entity.MapTo<ICExceptionDto>();
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<Entities.ICException>> GetAll(ICExceptionGetAllInput input)
        { 
            var query = JIT_ICException.GetAll().Where(w=>w.FSrcID==input.FID); 
            var data = await query.ToListAsync(); 
            return data; 
        }

        /// <summary>
        /// 添加和更改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CreateAndUpdate(ICExceptionDto input)
        {
            try
            {
                var entity = await JIT_ICException.GetAll()
                    .SingleOrDefaultAsync(p => p.FID == input.FID && p.FSrcID == input.FSrcID);

                var emp = await JIT_Employee.SingleAsync(p => p.FUserId == AbpSession.UserId);
                if (entity == null)
                {
                    entity = new DIME2Barcode.Entities.ICException()
                    {
                        FID =  Guid.NewGuid().ToString(),
                        FSrcID = input.FSrcID,
                        FBiller = emp.FName,
                        FNote = input.FNote,
                        FTime = input.FTime,
                        FRemark = input.FRemark,
                        FRecoverTime = input.FRecoverTime
                    };
                    await JIT_ICException.InsertAsync(entity);
                }
                else
                {
                    entity.FBiller = emp.FName;
                    entity.FNote = input.FNote;
                    entity.FTime = input.FTime;
                    entity.FRemark = input.FRemark;
                    entity.FRecoverTime = input.FRecoverTime;

                    await JIT_ICException.UpdateAsync(entity);
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
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> Delete(ICExceptionDto input)
        {
            try
            {
                var entity =
                    await JIT_ICException.GetAll().SingleOrDefaultAsync(p =>
                        p.FID == input.FID);
                await JIT_ICException.DeleteAsync(entity);
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
