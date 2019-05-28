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
using JIT.DIME2Barcode.Entities;
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
        public IRepository<ICMODispBill, string> icRepository { get; set; }

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
        public bool CreateOrUpdate(ICMaterialPickingCreateDto input)
        {
            try
            {
                var entity = Repository.GetAll().Where(p => p.FSrcID == input.FSrcID);
                foreach (var item in entity)
                {
                    Repository.Delete(item);
                }

                //List<DIME2Barcode.Entities.ICMaterialPicking>
                //    list = new List<DIME2Barcode.Entities.ICMaterialPicking>();
                foreach (var item in input.tmjx)
                {
                    DIME2Barcode.Entities.ICMaterialPicking IcMaterialPicking =
                        new DIME2Barcode.Entities.ICMaterialPicking()
                        {
                            FID = Guid.NewGuid().ToString(),
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
                    Repository.Insert(IcMaterialPicking);
                }

                var query = icRepository.GetAll().SingleOrDefault(s => s.FID == input.FID);
                query.FStatus = 1;
                icRepository.UpdateAsync(query);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
        public List<DIME2Barcode.Entities.ICMaterialPicking> Get(ICMaterialPickingGetDto input)
        {
            return Repository.GetAll().Where(p => p.FSrcID == input.FSrcID).ToList();
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
