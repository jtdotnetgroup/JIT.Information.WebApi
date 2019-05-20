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
using JIT.DIME2Barcode.TaskAssignment.ICException.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{

    /// <summary>
    /// 派工异常记录
    /// </summary>
    public class ICExceptionAppService : ApplicationService
    {
        //public ICExceptionAppService(IRepository<ICException, string> repository) : base(repository)
        //{
        //}
        //DIME2BarcodeContext context = new DIME2BarcodeContext();
        public IRepository<DIME2Barcode.Entities.ICException, string> Repository { get; set; }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ICExceptionDto> Get(ICExceptionInput input)
        {
            var entity = await Repository.GetAll()
                .SingleOrDefaultAsync(p => p.FID == input.FID && p.FSrcID == input.FSrcID);
            return entity.MapTo<ICExceptionDto>();
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ICExceptionDto>> GetAll(ICExceptionGetAllInput input)
        { 

            var query = Repository.GetAll().OrderBy(p => p.FID).PageBy(input);

            var count = await Repository.GetAll().CountAsync();

            var data = await query.ToListAsync();

            var list = data.MapTo<List<ICExceptionDto>>();

            return new PagedResultDto<ICExceptionDto>(count, list);

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
                var entity = await Repository.GetAll()
                    .SingleOrDefaultAsync(p => p.FID == input.FID && p.FSrcID == input.FSrcID);
                if (entity == null)
                {
                    entity = new DIME2Barcode.Entities.ICException()
                    {
                        FID = input.FID,
                        FSrcID = input.FSrcID,
                        FBiller = input.FBiller,
                        FNote = input.FNote,
                        FTime = input.FTime,
                        FRemark = input.FRemark,
                        FRecoverTime = input.FRecoverTime
                    };
                    await Repository.InsertAsync(entity);
                }
                else
                {
                    entity.FBiller = input.FBiller;
                    entity.FNote = input.FNote;
                    entity.FTime = input.FTime;
                    entity.FRemark = input.FRemark;
                    entity.FRecoverTime = input.FRecoverTime;

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
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> Delete(ICExceptionDto input)
        {
            try
            {
                var entity =
                    await Repository.GetAll().SingleOrDefaultAsync(p =>
                        p.FID == input.FID);
                await Repository.DeleteAsync(entity);
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
