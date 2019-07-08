using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    public class TB_BadItemRelationAppService: ApplicationService
    {

        public IRepository<TB_BadItemRelation,int> Repository { get; set; }
        public IRepository<t_SubMesType_Sync,int> SubMesTypeRepository { get; set; }//辅助资料表
        public IRepository<t_SubMessage_Sync, int> SubMessageRepository { get; set; }//辅助资料类型表
        public IRepository<t_ICItem,int> ICItemRepository { get; set; }
      

        /// <summary>
        /// 工序不良项目表
        /// </summary>
        /// <returns></returns> 
        public List<TB_BadItemRelation> GetAll()
        {
            return Repository.GetAll().ToList();
        }


        /// <summary>
        /// 查询工序
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeSubMessageDto>> GetTree()
        {
            var Context = SubMessageRepository.GetDbContext() as Dime2barcodeContext;

            List<TreeSubMessageDto> list = new List<TreeSubMessageDto>();

            var query = from a in Context.t_SubMessage
                join b in Context.t_SubMesType on a.FTypeID equals b.FTypeID into bd
                from ac in bd.DefaultIfEmpty()
                where ac.FName.Contains("工序资料")
                select new
                {
                    a.FInterID,
                    a.FID,
                    a.FName
                };

            foreach (var item in await query.ToListAsync())
            {
                TreeSubMessageDto td = new TreeSubMessageDto();
                td.key = item.FInterID.ToString();
                td.title = item.FName;
                list.Add(td);
            }

            return list;
        }


        [AbpAuthorize(ProductionPlanPermissionsNames.BadProjects_Get)]
        public async  Task<PagedResultDto<TB_BadItemRelationDto>> GetAllBadItemRelation(TB_BadItemRelationGetAllInput input)
        {
            #region 暂时不用，因为数据还没同步过来

            //var context = Repository.GetDbContext() as ProductionPlanMySqlDbContext;

            //var query = from a in context.TB_BadItemRelation
            //            join b in context.t_ICItem on a.FItemID equals b.FItemID into ab
            //            from a1 in ab.DefaultIfEmpty()
            //            join c in context.t_SubMessage on a.FOperID equals c.FInterID into ac
            //            from a2 in ac.DefaultIfEmpty()
            //            select new TB_BadItemRelationDto()
            //            {
            //                FID = a.FID,
            //                FItemName = a1.FName,//产品名称
            //                FItemID = a1.FItemID,//产品Id
            //                FOperID = a.FOperID,//工序ID
            //                FNumber = a.FNumber,//不良项目代码
            //                FName = a.FName,//不良项目名称
            //                FDeleted = a.FDeleted, //是否禁用
            //                FRemark = a.FRemark,//备注
            //                FOperName = a2.FName
            //            };

            #endregion
            #region 旧代码
            //var query = from item in context.t_ICItem
            //            join b in context.TB_BadItemRelation on item.FItemID equals b.FItemID 
            //            join c in context.t_SubMessage on b.FOperID equals c.FInterID                       
            //            select new TB_BadItemRelationDto()
            //            {
            //                FID=b.FID,
            //                FItemName = item.FName,//产品名称
            //                FItemID=b.FItemID,//产品Id
            //                FOperID=b.FOperID,//工序ID
            //                FNumber=b.FNumber,//不良项目代码
            //                FName=b.FName,//不良项目名称
            //                FDeleted=b.FDeleted, //是否禁用
            //                FRemark=b.FRemark,//备注
            //                FOperName= c.FName
            //            };


            #endregion

            var process = SubMessageRepository.GetAll().Include(p => p.SubMessageType)
                .Where(p => p.SubMessageType.FName.Contains("工序资料"));

            var processIdList = (from a in process
                select a.FInterID).ToList();

            var query = Repository.GetAll();
            // 过滤工序不存的记录
            query = from a in query
                where processIdList.Contains(a.FOperID)
                select a;

            if (input.FOperID == 0)
            {
                var count = query.Count();

                var data = await query.OrderBy(p => p.FID).PageBy(input).Include(p => p.Operate).ToListAsync();

                var list = data.MapTo<List<TB_BadItemRelationDto>>();

                return new PagedResultDto<TB_BadItemRelationDto>(count, list);
            }
            else
            {
                var count = query.Count(p => p.FOperID == input.FOperID);

                var data = await query.Where(p => p.FOperID == input.FOperID).OrderBy(p => p.FID).PageBy(input).Include(p => p.Operate).ToListAsync();

                var list = data.MapTo<List<TB_BadItemRelationDto>>();
                return new PagedResultDto<TB_BadItemRelationDto>(count, list);
            }



        }


        /// <summary>
        /// 新增不良项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BadProjects_Create)]
        public async Task<TB_BadItemRelation> Create(TB_BadItemRelationCreateAndEditDto input)
        {
            var entity = input.MapTo<TB_BadItemRelation>();

            var count= await Repository.InsertAsync(entity);

            return count;

        }

        /// <summary>
        /// 修改不良项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BadProjects_Update)]
        public Task<TB_BadItemRelation> Update(TB_BadItemRelationCreateAndEditDto input)
        {
            var entity = input.MapTo<TB_BadItemRelation>();
            var count = Repository.UpdateAsync(entity);
            return count;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BadProjects_Delete)]
        public async Task<int> Delete(TB_BadItemRelationCreateAndEditDto input)
        {
            try
            {
                var entity = Repository.GetAll().SingleOrDefault(p => p.FID == input.FID);
                entity.FDeleted = true;

                await Repository.UpdateAsync(entity);

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }      
        }


        /// <summary>
        /// 通过名称查找产品名称
        /// </summary>
        /// <param name="FName"></param>
        /// <returns></returns>
        public async Task<List<t_ICItem>> GetICItem(string  FName)
        {
            var query = await ICItemRepository.GetAll().Where(p => p.FName.Contains(FName)).OrderBy(p=>p.FName).Take(10).ToListAsync();

            return query.MapTo<List<t_ICItem>>();
        }
    }
}
