using Abp.Application.Services;
using Abp.Domain.Repositories;
using JIT.DIME2Barcode.TaskAssignment.T_PrintTemplate.Dtos;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 打印模板设置
    /// </summary>
    public class T_PrintTemplateAppService : AsyncCrudAppService<T_PrintTemplate, T_PrintTemplateDto, string, T_PrintTemplateGetAllInput, T_PrintTemplateDto, T_PrintTemplateDto, T_PrintTemplateDto, T_PrintTemplateDto>
    {
        public T_PrintTemplateAppService(IRepository<T_PrintTemplate, string> repository) : base(repository)
        {
        }
    }
    //{
        //    public IRepository<Entities.VW_MOBillList, string>  { get; set; }
        //    /// <summary>Repository
        //    /// 获取明细
        //    /// </summary>
        //    /// <param name="input"></param>
        //    /// <returns></returns>
        //    public async Task<T_PrintTemplateDto> Get(T_PrintTemplateDto input)
        //    {
        //        var entity = await context.T_PrintTemplate.SingleOrDefaultAsync(p => p.FInterID == input.FInterID);
        //        return entity.MapTo<T_PrintTemplateDto>();
        //    }
        //    /// <summary>
        //    /// 获取全部数据
        //    /// </summary>
        //    /// <param name="input"></param>
        //    /// <returns></returns>

        //    public async Task<PagedResultDto<T_PrintTemplateDto>> GetAll(T_PrintTemplateGetAllInput input)
        //    {
        //        var query = from a in context.T_PrintTemplate
        //                    select a;

        //        query = query.OrderBy(p => p.FTranType).PageBy(input);

        //        var count = await context.T_PrintTemplate.CountAsync();

        //        var data = await query.ToListAsync();

        //        var list = data.MapTo<List<T_PrintTemplateDto>>();

        //        return new PagedResultDto<T_PrintTemplateDto>(count, list);

        //    }
        //    /// <summary>
        //    /// 添加
        //    /// </summary>
        //    /// <param name="input"></param>
        //    /// <returns></returns>
        //    public async Task<T_PrintTemplateDto> Create(T_PrintTemplateDto input)
        //    {
        //        var entity = input.MapTo<JITEF.DIME2Barcode.T_PrintTemplate>();

        //        context.T_PrintTemplate.Attach(entity);

        //        context.T_PrintTemplate.Add(entity);

        //        await context.SaveChangesAsync();

        //        return entity.MapTo<T_PrintTemplateDto>();

        //    }
        //    /// <summary>
        //    /// 修改
        //    /// </summary>
        //    /// <param name="input"></param>
        //    /// <returns></returns>
        //    public async Task<T_PrintTemplateDto> Update(T_PrintTemplateDto input)
        //    {
        //        var entity = input.MapTo<JITEF.DIME2Barcode.T_PrintTemplate>();

        //        context.T_PrintTemplate.Attach(entity);

        //        context.Entry(entity).State = EntityState.Modified;

        //        await context.SaveChangesAsync();

        //        return entity.MapTo<T_PrintTemplateDto>();
        //    }
        //    /// <summary>
        //    /// 删除
        //    /// </summary>
        //    /// <param name="input"></param>
        //    /// <returns></returns>
        //    public async Task Delete(T_PrintTemplateDto input)
        //    {
        //        var entity = await
        //            context.T_PrintTemplate.SingleOrDefaultAsync(p =>
        //                p.FTranType == input.FTranType && p.FInterID == input.FInterID);

        //        context.T_PrintTemplate.Attach(entity);

        //        context.Entry(entity).State = EntityState.Deleted;

        //        await context.SaveChangesAsync();
        //    }

    //}
}
