using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using JIT.DIME2Barcode.TaskAssignment.ICException.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{

    /// <summary>
    /// 派工异常记录
    /// </summary>
    public class ICExceptionAppService : AsyncCrudAppService<ICException,ICExceptionDto, string, ICExceptionGetAllInput,
        ICExceptionDto, ICExceptionDto, ICExceptionDto, ICExceptionDto>, IAsyncCrudAppService<ICExceptionDto, string, ICExceptionGetAllInput,
        ICExceptionDto, ICExceptionDto, ICExceptionDto, ICExceptionDto>
    {
        public ICExceptionAppService(IRepository<ICException, string> repository) : base(repository)
        {
        }
        //DIME2BarcodeContext context = new DIME2BarcodeContext();

        ///// <summary>
        ///// 获取明细
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<ICExceptionDto> Get(ICExceptionDto input)
        //{
        //    var entity = await context.ICException.SingleOrDefaultAsync(p => p.FID == input.FID);
        //    return entity.MapTo<ICExceptionDto>();
        //}
        ///// <summary>
        ///// 获取全部数据
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>

        //public async Task<PagedResultDto<ICExceptionDto>> GetAll(ICExceptionGetAllInput input)
        //{
        //    var query = from a in context.ICException
        //                select a;

        //    query = query.OrderBy(p => p.FID).PageBy(input);

        //    var count = await context.ICException.CountAsync();

        //    var data = await query.ToListAsync();

        //    var list = data.MapTo<List<ICExceptionDto>>();

        //    return new PagedResultDto<ICExceptionDto>(count, list);

        //}
        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<ICExceptionDto> Create(ICExceptionDto input)
        //{
        //    var entity = input.MapTo<JITEF.DIME2Barcode.ICException>();

        //    context.ICException.Attach(entity);

        //    context.ICException.Add(entity);

        //    await context.SaveChangesAsync();

        //    return entity.MapTo<ICExceptionDto>();

        //}
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<ICExceptionDto> Update(ICExceptionDto input)
        //{
        //    var entity = input.MapTo<JITEF.DIME2Barcode.ICException>();

        //    context.ICException.Attach(entity);

        //    context.Entry(entity).State = EntityState.Modified;

        //    await context.SaveChangesAsync();

        //    return entity.MapTo<ICExceptionDto>();
        //}
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task Delete(ICExceptionDto input)
        //{
        //    var entity = await
        //        context.ICException.SingleOrDefaultAsync(p =>
        //            p.FID == input.FID);

        //    context.ICException.Attach(entity);

        //    context.Entry(entity).State = EntityState.Deleted;

        //    await context.SaveChangesAsync();
        //}


    }
}
