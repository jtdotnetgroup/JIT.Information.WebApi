using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.VW_RoleUserAll.Dtos;
using JIT.InformationSystem.Users.Dto;
using Microsoft.EntityFrameworkCore;
using PagedUserResultRequestRoleStaicDto = JIT.DIME2Barcode.TaskAssignment.VW_RoleUserAll.Dtos.PagedUserResultRequestRoleStaicDto;

namespace JIT.DIME2Barcode.AppService
{
    public class VW_RoleUserAllAppService: ApplicationService
    {

        public IRepository<VW_RoleUserAll> Repository { get; set; }
        public  IRepository<UserRole, long> UserRoleRepository { get; set; }

        /// <summary>
        /// 查询角色管理的成员管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<VW_RoleUserAllDto>> GetRoleUser(PagedUserResultRequestRoleStaicDto input)
        {

            IQueryable<VW_RoleUserAll> query;

            int count = 0;
            List<VW_RoleUserAllDto> list = new List<VW_RoleUserAllDto>();
            List<VW_RoleUserAll> data = new List<VW_RoleUserAll>();


            if (input.RoleStaic == 1)//查询全部
            {
                query = Repository.GetAll().Where(p=>p.RoleId== input.RoleId&&p.TenantId==null);
                count = await query.CountAsync();
                data = await query.OrderBy(u => u.UserName).PageBy(input).ToListAsync();
                list = data.MapTo<List<VW_RoleUserAllDto>>();
            }
            //else if (input.RoleStaic == 2)//查询是角色成员
            //{
            //    query = Repository.GetAll().Where(p=>p.RoleId==input.RoleId);
            //    count = await query.CountAsync();
            //    data = await query.OrderBy(u => u.UserName).PageBy(input).ToListAsync();
            //    list = data.MapTo<List<VW_RoleUserAllDto>>();        
            //}
            else//查询不是角色成员
            {   
               List<VW_RoleUserAll> lists = new List<VW_RoleUserAll>();
               List<VW_RoleUserAll> ListRole = Repository.GetAll().Where(p => p.RoleId != input.RoleId || p.RoleId == null && p.TenantId == null).ToList();
           
               foreach (var itme in ListRole)
               {
                   if (itme.RoleId != null)
                   {
                       var sed = Repository.GetAll().FirstOrDefault(p => p.Id == itme.Id && p.RoleId == input.RoleId);
                       if (sed==null)
                       {
                           lists.Add(itme);
                       }
                    }
                   else
                   {
                       lists.Add(itme);
                    }
               }

               var datas = lists.OrderBy(u => u.UserName).Skip(input.SkipCount)
                   .Take(input.MaxResultCount).ToList();

                count = lists.Count();

              
                list = datas.MapTo<List<VW_RoleUserAllDto>>();
            }
            return new PagedResultDto<VW_RoleUserAllDto>(count, list);

        }

        /// <summary>
        /// 新建用户角色管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Create(VW_RoleUserAllCreateDto input)
        {

            try
            {
                foreach (var item in input.UserListsId)
                {
                    if (item.UserBool)
                    {
                        //新增加userrole true

                        var queryUsre = UserRoleRepository.GetAll()
                            .FirstOrDefault(p => p.UserId == item.UserID && p.RoleId == input.Id) ??new UserRole();

                        queryUsre.UserId = item.UserID;
                        queryUsre.RoleId = input.Id;//角色ID
                        queryUsre.CreationTime = DateTime.Now;
                        queryUsre.CreatorUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0;
                        
                        var query = UserRoleRepository.InsertOrUpdate(queryUsre);
                    }
                    else
                    {
                        //删除userrole false
                        var query = UserRoleRepository.GetAll().FirstOrDefault(p => p.UserId == item.UserID && p.RoleId == input.Id) ??new UserRole();

                         UserRoleRepository.Delete(query);
                    }

                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
          
        }

    }
}
