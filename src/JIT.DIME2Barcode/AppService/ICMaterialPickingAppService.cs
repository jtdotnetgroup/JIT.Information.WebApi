using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Castle.Components.DictionaryAdapter;
using JIT.DIME2Barcode.TaskAssignment.ICException.Dtos;
using JIT.JIT.TaskAssignment.ICMaterialPicking.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JIT.JIT.TaskAssignment.ICMaterialPicking
{
    /// <summary>
    /// 派工异常记录  IAsyncCrudAppService《全部，添加，修改，明细，删除》
    /// </summary>
    public class ICMaterialPickingAppService : ApplicationService
    { 
        public IRepository<DIME2Barcode.Entities.ICMaterialPicking, string> Repository { get; set; }

        //DIME2BarcodeContext context =new DIME2BarcodeContext();
        ///// <summary>
        ///// 获取全部数据
        ///// </summary>
        ///// <param name="input">条件</param>
        ///// <returns></returns>
        //public async Task<PagedResultDto<ICMaterialPickingDto>> GetAll(ICMaterialPickingGetAllInput input)
        //{
        //    var query = from a in context.ICMaterialPicking.OrderBy(p => p.FID).PageBy(input)
        //        select a;

        //    query = query.OrderBy(p => p.FID).PageBy(input);

        //    var count = await context.ICMaterialPicking.CountAsync();

        //    var data = await query.ToListAsync();

        //    var list = data.MapTo<List<ICMaterialPickingDto>>();

        //    return new PagedResultDto<ICMaterialPickingDto>(count, list);

        //}
        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public async Task<bool> CreateOrUpdate(ICMaterialPickingCreateDto input)
        {
            try
            {
                var entity = await Repository.GetAll()
                    .Where(p => p.FID == input.FID && p.FSrcID == input.FSrcID).ToListAsync();
                foreach (var item in entity)
                {
                    await Repository.DeleteAsync(item);
                }

                List<DIME2Barcode.Entities.ICMaterialPicking>
                    list = new List<DIME2Barcode.Entities.ICMaterialPicking>();
                foreach (var item in input.tmjx)
                {
                    DIME2Barcode.Entities.ICMaterialPicking IcMaterialPicking = new DIME2Barcode.Entities.ICMaterialPicking()
                    {
                        FID = input.FID,
                        FSrcID = input.FSrcID,
                        FEntryID = input.tmjx.IndexOf(item) + 1,
                        FItemID = item.FItemID,
                        FUnitID = item.FUnitID,
                        FBatchNo = item.FBatchNo,
                        FAuxQty = 0,
                        FBiller = AbpSession.UserId.ToString(),
                        FDate = DateTime.Now,
                        FNote = input.FNote,
                    };
                    await Repository.InsertAsync(IcMaterialPicking);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="input">条件</param>
        ///// <returns></returns>
        //public async Task<ICMaterialPickingDto> Update(ICMaterialPickingUpdateDto input)
        //{
        //    var entity = input.MapTo<JITEF.DIME2Barcode.ICMaterialPicking>();

        //    context.ICMaterialPicking.Attach(entity);

        //    context.Entry(entity).State = EntityState.Modified;

        //    await context.SaveChangesAsync();

        //    return entity.MapTo<ICMaterialPickingDto>();
        //}
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="input">条件</param>
        /// <returns></returns>
        public async Task<List<DIME2Barcode.Entities.ICMaterialPicking>> Get(ICMaterialPickingGetDto input)
        {
            var entity = Repository.GetAll().SingleOrDefault(p => p.FID == input.FID && p.FSrcID == input.FSrcID);
            return entity.MapTo<List<DIME2Barcode.Entities.ICMaterialPicking>>();
        }
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="input">条件</param>
        ///// <returns></returns>
        //public async Task Delete(ICMaterialPickingDeleteDto input)
        //{
        //    var entity = await
        //        context.ICMaterialPicking.SingleOrDefaultAsync(p =>
        //            p.FID == input.FID);

        //    context.ICMaterialPicking.Attach(entity);

        //    context.Entry(entity).State = EntityState.Deleted;

        //    await context.SaveChangesAsync();
        //}
        ///// <summary>
        ///// 删除全部
        ///// </summary>
        ///// <param name="input">条件</param>
        ///// <returns></returns>
        //public async Task AllDelete(ICMaterialPickingAllDeleteDto input)
        //{
        //    var entity = context.ICMaterialPicking.Where(w => input.Id.Any(a => a.Equals(w.FID)));
        //    context.Entry(entity).State = EntityState.Deleted;
        //    await context.SaveChangesAsync();
        //}
    }

}
