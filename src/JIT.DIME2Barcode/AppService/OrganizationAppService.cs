using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using JIT.DIME2Barcode.SystemSetting.Organization.ISpecification;

namespace JIT.DIME2Barcode.SystemSetting.Organization
{
    public class OrganizationAppService:ApplicationService
        //: AsyncCrudAppService<OrganizationUnit, OrganizationDto, long, OrganizationGetAllInput, OrganizationCreateInput, OrganizationDto, OrganizationDto, OrganizationDeleteInput>
    {

        public IRepository<OrganizationUnit, int> _repository { get; set; }

        /// <summary>
        /// 根据父节点获取子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public async Task<List<OrganizationDto>>  GetChildMenuList(int ParentID)
        {
     
            var quers = _repository.GetAll().Where(p=> p.IsDeleted == false).ToList();         
            
            var list = quers.MapTo<List<OrganizationDto>>();  

            var result = list.Where(x => x.ParentId == ParentID);
            return result.ToList();


        }

        /// <summary>
        /// 返回拼接的节点信息
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public async Task<List<OrganizationDtoTest>> GetTreeList(int ParentID)
        {




            List<OrganizationDtoTest> TreeList = new List<OrganizationDtoTest>();
            List<OrganizationDto> ModelList = await GetChildMenuList(ParentID);
            foreach (var item in ModelList)
            {
                OrganizationDtoTest m = new OrganizationDtoTest();
                m.Id = item.Id;
                m.title = item.DisplayName;
                m.key = item.Code;
                m.ParentId = item.ParentId;
                m.TenantId = item.TenantId;
                //m.Code = item.Code;
                //m.DisplayName = item.DisplayName;
                m.value = item.Id.ToString();
                m.label = item.DisplayName;            
                m.children = await GetTreeList(int.Parse(item.Id.ToString()));
                TreeList.Add(m);
            }
            return TreeList;
        }


        public async Task<List<OrganizationDtoTest>> GetAll(OrganizationGetAllInput input)
        {
            var query = _repository.GetAll();
            if (input.OrganizationType.HasValue)
            {
                //过滤组织类型
                OrganizationTypeSpecification wcsf=new OrganizationTypeSpecification(input.OrganizationType.Value);
                query = query.Where(wcsf);
            }

            var data = await query.ToListAsync();
            return data.MapTo<List<OrganizationDtoTest>>();
        }
       

        /// <summary>
        /// 新增组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Create(OrganizationCreateInput input)
        {

           

            var entity = new OrganizationUnit()
            {
                Code = input.Code,
                ParentId = int.Parse(input.ParentId.ToString()==null?"0": input.ParentId.ToString()),
                TenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0,
                CreationTime = DateTime.Now,
                CreatorUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0,
                DisplayName = input.DisplayName,
                IsDeleted = false,
                OrganizationType = Enum.Parse<PublicEnum.OrganizationType>(input.OrganizationType.ToString()),//组织类型
                DataBaseConnection = input.DataBaseConnection,//数据库连接
                ERPOrganizationLeader = input.ERPOrganizationLeader == null ? 0 : input.ERPOrganizationLeader,//组织负责人
                ERPOrganization = input.ERPOrganization == null ? 0 : input.ERPOrganization,
                Remark =input.Remark,
            };
       
            return await _repository.InsertAndGetIdAsync(entity);

        }

        /// <summary>
        /// 返回枚举 组织类型 list集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectOptio>> GetSelectOptio()
        {
            List<SelectOptio> list = new List<SelectOptio>();
            foreach (var e in Enum.GetValues(typeof(PublicEnum.OrganizationType)))//枚举转List
            {
                SelectOptio s = new SelectOptio();
                //object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                s.Id = Convert.ToInt32(e);
                s.Name = e.ToString();
                list.Add(s);
            }
            return  list;
        }
     
        public async Task<List<OrganizationDto>>  GetCore(string Code)
        {
            var entity = _repository.GetAll().Where(p => p.Code == Code && p.IsDeleted==false).ToList();
            return entity.MapTo<List<OrganizationDto>>();

        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Delete(OrganizationDeleteDto input)
        {
            var count = 0;
            var query = _repository.GetAll().Where(p => p.Id == input.Id)
                .Include(p => p.Children);

            var entity = await query.SingleOrDefaultAsync(p => true);

            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletionTime = DateTime.Now;
                entity.DeleterUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0;
                count++;
                foreach (var c in entity.Children)
                {
                    c.IsDeleted = true;
                    c.DeletionTime = DateTime.Now;
                    c.DeleterUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0;
                    count++;
                }

                await _repository.UpdateAsync(entity);
            }

            return count;
        }

        /// <summary>
        /// 查找子点所在的公司或集团
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected  OrganizationUnit GetCompany(OrganizationUnit node,List<OrganizationUnit> treeList)
        {
            //最终返回的结果
            OrganizationUnit result = null;
            //判断传入的节点是否是公司，如果是则返回
            if (node.OrganizationType == PublicEnum.OrganizationType.公司||node.OrganizationType==PublicEnum.OrganizationType.集团)
            {
                return node;
            }
            //判断传入节点是否有父节点，如果有，则执行递归
            if (node.ParentId != null)
            {
                var parent = treeList.SingleOrDefault(p => p.Id == node.ParentId);

                result = GetCompany(parent, treeList);
            }

            return result;
        }



    }
}