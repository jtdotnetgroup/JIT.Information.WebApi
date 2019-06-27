using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using JIT.DIME2Barcode.SystemSetting.Organization.ISpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using JIT.DIME2Barcode.AppService;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.SystemSetting.Employee.Dtos;

namespace JIT.DIME2Barcode.SystemSetting.Organization
{
    public class OrganizationAppService:ApplicationService
        //: AsyncCrudAppService<OrganizationUnit, OrganizationDto, long, OrganizationGetAllInput, OrganizationCreateInput, OrganizationDto, OrganizationDto, OrganizationDeleteInput>
    {

        public IRepository<t_OrganizationUnit, int> _repository { get; set; }

        public  EmployeeAppService EmployeeApp { get; set; }

        /// <summary>
        /// 根据父节点获取子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeAdd)]
        public async Task<List<OrganizationDto>>  GetChildMenuList(int ParentID)
        {
     
            var quers =await _repository.GetAll().Where(p=> p.IsDeleted == false).ToListAsync();         
            
            var list = quers.MapTo<List<OrganizationDto>>();

            var result = list.Where(x => x.ParentId == ParentID);
            return result.ToList();


        }

        /// <summary>
        /// 返回拼接的节点信息
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeGet)]
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


        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeGet)]
        public async Task<List<OrganizationDtoTest>> GetAll(OrganizationGetAllInput input)
        {
            var query = _repository.GetAll().Where(p=>p.IsDeleted.HasValue&&!p.IsDeleted.Value);
            if (input.OrganizationType.HasValue)
            {
                //过滤组织类型
                OrganizationTypeSpecification wcsf=new OrganizationTypeSpecification(input.OrganizationType.Value);
                query = query.Where(wcsf);
            }

            if (input.isWorkCenter)
            {
                query = query.Where(p => p.FWorkshopType);
            }

            var data = await query.ToListAsync();
            return data.MapTo<List<OrganizationDtoTest>>();
        }


        /// <summary>
        /// 新增组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeAdd)]
        public async Task<int> Create(OrganizationCreateInput input)
        {
            try
            {
                var entity = new t_OrganizationUnit()
                {
                    Code = input.Code,
                    ParentId = int.Parse(input.ParentId.ToString() == null ? "0" : input.ParentId.ToString()),
                    TenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0,
                    CreationTime = DateTime.Now,
                    CreatorUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0,
                    DisplayName = input.DisplayName,
                    IsDeleted = false,
                    OrganizationType = Enum.Parse<PublicEnum.OrganizationType>(input.OrganizationType.ToString()),//组织类型
                    DataBaseConnection = input.DataBaseConnection,//数据库连接
                    ERPOrganizationLeader = input.ERPOrganizationLeader == null ? 0 : input.ERPOrganizationLeader,//组织负责人
                    ERPOrganization = input.ERPOrganization == null ? 0 : input.ERPOrganization,
                    Remark = input.Remark,
                    FWorkshopType = input.FWorkshopType

                };

                return await _repository.InsertAndGetIdAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;

        }

        //修改组织
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeUpdate)]
        public async Task<t_OrganizationUnit> Update(OrganizationCreateInput input)
        {
            var entity = input.MapTo<t_OrganizationUnit>();
            entity.Id = input.Id;
            entity.TenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0;
            entity.IsDeleted = false;
            entity.LastModificationTime=DateTime.Now;
            entity.LastModifierUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0;
            entity.CreationTime = input.CreationTime;
            entity.CreatorUserId = input.CreatorUserId;
            return await _repository.UpdateAsync(entity); ;
        }


        /// <summary>
        /// 返回枚举 组织类型 list集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeGet)]
        public  List<SelectOptio> GetSelectOptio()
        {
            List<SelectOptio> list = new List<SelectOptio>();
            foreach (var e in Enum.GetValues(typeof(PublicEnum.OrganizationType)))//枚举转List
            {
                SelectOptio s = new SelectOptio();            
                s.Id = Convert.ToInt32(e);
                s.Name = e.ToString();
                list.Add(s);
            }
            return  list;
        }

        /// <summary>
        /// 通过当前节点去获取节点信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeGet)]
        public async Task<t_OrganizationUnit> Get(OrganizationDeleteDto input)
        {
            var entity = await _repository.GetAll().SingleOrDefaultAsync(p => p.Id == input.Id && p.IsDeleted == false);
            return entity.MapTo<t_OrganizationUnit>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeDelete)]
        public async Task<int> Delete(OrganizationDeleteDto input)
        {
            var count = 0;
            var query = _repository.GetAll().Where(p => p.Id == input.Id)
                .Include(p => p.Children);

            var entity = await query.SingleOrDefaultAsync(p => true);

            var ORByID = _repository.GetAll().SingleOrDefault(p => p.Id == entity.ParentId);

            var EmployeeByID = EmployeeApp.GetOneselfAndJunior(new int[] {input.Id});

            List<Entities.Employee> EmployeeList=new EditableList<Entities.Employee>();

            if (EmployeeByID!=null)
            {
                 EmployeeList = EmployeeApp._ERepository.GetAll()
                    .Where(p => p.IsDeleted == false && EmployeeByID.Contains(p.FDepartment)).ToList();
            }

          
            if (entity != null)
            {
         
                foreach (var e in EmployeeList)
                {
                    if (ORByID!=null)
                    {
                        e.FDepartment = ORByID.Id;
                    }
                    else
                    {
                        e.FDepartment= _repository.GetAll().FirstOrDefault(p => p.Id == entity.ParentId&&p.IsDeleted==false).Id;
                    }
                 
                  EmployeeApp._ERepository.Update(e);
                }

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
        protected  t_OrganizationUnit GetCompany(t_OrganizationUnit node,List<t_OrganizationUnit> treeList)
        {
            //最终返回的结果
            t_OrganizationUnit result = null;
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

        /// <summary>
        /// 通过父节点ID获取所有子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        //public async Task<List<t_OrganizationUnit>> GetChildren(int parentId)
        //{
        //    var list =await  _repository.GetAllListAsync();

        //    var result = list.Where(p => p.ParentId == parentId||p.Id==parentId);



        //}

        /// <summary>
        /// 通过父节点查出所有子节点
        /// </summary>
        /// <param name="node">父节点</param>
        /// <param name="list">结果</param>
        /// <returns></returns>
        //protected List<t_OrganizationUnit> GetChildrent(t_OrganizationUnit parent, List<t_OrganizationUnit> list)
        //{
            
        //}



    }
}