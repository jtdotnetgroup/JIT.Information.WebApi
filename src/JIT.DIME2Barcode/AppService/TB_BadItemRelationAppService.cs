﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.TB_BadItemRelation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    public class TB_BadItemRelationAppService: ApplicationService
    {

        public IRepository<TB_BadItemRelation,int> Repository { get; set; }
        public IRepository<t_SubMesType,int> SubMesTypeRepository { get; set; }//辅助资料表
        public IRepository<t_SubMessage,int> SubMessageRepository { get; set; }//辅助资料类型表
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
            var Context = SubMessageRepository.GetDbContext() as ProductionPlanMySqlDbContext;

            List<TreeSubMessageDto> list = new List<TreeSubMessageDto>();
            var query = from a in Context.t_SubMessage
                        join b in Context.t_SubMesType on a.FTypeID equals b.FTypeID
                where b.FName=="工序"
                select new
                {
                    a.FInterID,
                    a.FID,
                    a.FName
                };

            foreach (var item in query.ToList())
            {
                TreeSubMessageDto td = new TreeSubMessageDto();
                td.key = item.FInterID.ToString();
                td.title = item.FName;
                list.Add(td);
            }

            return list;
        }


        public async  Task<PagedResultDto<TB_BadItemRelationDto>> GetAllBadItemRelation(TB_BadItemRelationGetAllInput input)
        {

            var context = Repository.GetDbContext() as ProductionPlanMySqlDbContext;

            var query = from item in context.t_ICItem
                        join b in context.TB_BadItemRelation on item.FItemID equals b.FItemID 
                        join c in context.t_SubMessage on b.FOperID equals c.FInterID                       
                        select new TB_BadItemRelationDto()
                        {
                            FID=b.FID,
                            FItemName = item.FName,//产品名称
                            FItemID=b.FItemID,//产品Id
                            FOperID=b.FOperID,//工序ID
                            FNumber=b.FNumber,//不良项目代码
                            FName=b.FName,//不良项目名称
                            FDeleted=b.FDeleted, //是否禁用
                            FRemark=b.FRemark,//备注
                            FOperName= c.FName
                        };

            if (input.FOperID == 0)
            {
                var count = Repository.GetAll().Count();

                var data = query.OrderBy(p => p.FID).Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToList();

                var list = data.MapTo<List<TB_BadItemRelationDto>>();
                return new PagedResultDto<TB_BadItemRelationDto>(count, list);
            }
            else
            {
                var count = Repository.GetAll().Count(p => p.FOperID == input.FOperID);

                var data = query.Where(p => p.FOperID == input.FOperID).OrderBy(p => p.FID).Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToList();

                var list = data.MapTo<List<TB_BadItemRelationDto>>();
                return new PagedResultDto<TB_BadItemRelationDto>(count, list);
            }

            

        

        }


        /// <summary>
        /// 新增不良项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        public async Task<List<t_ICItem>> GetICItem(string  FName )
        {
            var query = ICItemRepository.GetAll().Where(p => p.FName.Contains(FName)).OrderBy(p=>p.FName).Take(10).ToList();

            return query.MapTo<List<t_ICItem>>();
        }
    }
}
