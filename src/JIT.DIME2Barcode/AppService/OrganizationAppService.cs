using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Organizations;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VW_ICMOInspectBillList.Dtos;
using Newtonsoft.Json;

namespace JIT.DIME2Barcode.SystemSetting.Organization
{
    public class OrganizationAppService:ApplicationService
        //: AsyncCrudAppService<OrganizationUnit, OrganizationDto, long, OrganizationGetAllInput, OrganizationCreateInput, OrganizationDto, OrganizationDto, OrganizationDeleteInput>
    {

        public IRepository<OrganizationUnitsJT, int> _repository { get; set; }

        public IRepository<OrganizationUnit,long> _ORepository { get; set; }

    


        /// <summary>
        /// 根据父节点获取子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public async Task<List<OrganizationDto>>  GetChildMenuList(int ParentID)
        {
     
            var quers = _repository.GetAll().ToList();         
            
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
                m.Code = item.Code;
                m.DisplayName = item.DisplayName;
                m.children = await GetTreeList(int.Parse(item.Id.ToString()));
                TreeList.Add(m);
            }
            return TreeList;
        }

       

        public async Task<int> Create(OrganizationCreateInput input)
        {
            var ss =string.Empty;
            var Append = string.Empty;

            if (input.ParentId!=0)
            {
                var parentIdcore = _repository.GetAll().SingleOrDefault(p=>p.Id== input.ParentId);

                Append = OrganizationUnit.CreateCode(Convert.ToInt32(input.Code));

                ss = OrganizationUnit.AppendCode(parentIdcore.Code, Append);
            }
            else
            {
                ss = OrganizationUnit.CreateCode(Convert.ToInt32(input.Code));               
            }

            //var UserId = this.AbpSession.UserId;       
            var entity = new OrganizationUnitsJT()
            {
                Code = ss,
                ParentId = int.Parse(input.ParentId.ToString()),
                TenantId = this.AbpSession.TenantId,
                CreationTime = DateTime.Parse(DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.sss")),
                CreatorUserId =1,
                DisplayName = input.DisplayName,
                IsDeleted = false,
                OrganizationType = input.OrganizationType
            };
       
            return await _repository.InsertAndGetIdAsync(entity);

        }

        //

    }
}