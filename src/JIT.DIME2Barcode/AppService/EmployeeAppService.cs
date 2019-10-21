﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using CommonTools;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Employee.Dtos;
using JIT.DIME2Barcode.SystemSetting.Organization.Dtos;
using JIT.DIME2Barcode.TaskAssignment.VWEmployees;
using JIT.InformationSystem.Authorization.Roles;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.EntityFrameworkCore;
using JIT.InformationSystem.Users;
using JIT.InformationSystem.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp;
using Abp.UI;
using Castle.Components.DictionaryAdapter;
using JIT.DIME2Barcode.Permissions;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.AppService
{
    
    public class EmployeeAppService : ApplicationService
    {
        public IRepository<Employee, int> _ERepository { get; set; }

        public IRepository<User, long> _UserRepository { get; set; }

        public IRepository<Role, int> _UserRoleRepository { get; set; }

        public IRepository<t_OrganizationUnit, int> _Repository { get; set; }

        public IRepository<VW_Employee, int> _VwRepository { get; set; }

        public IPasswordHasher<User> PasswordHasher { get; set; }

        public IUserAppService UserAppService { get; set; }


        //返回公司
        protected t_OrganizationUnit GetCompany(t_OrganizationUnit node, List<t_OrganizationUnit> terrList)
        {
            t_OrganizationUnit reslut = null;
            if (node.OrganizationType == PublicEnum.OrganizationType.公司 ||
                node.OrganizationType == PublicEnum.OrganizationType.集团)
            {
                return node;
            }

            if (node.ParentId != 0)
            {
                var parent = terrList.SingleOrDefault(p => p.Id == node.ParentId);
                reslut = GetCompany(parent, terrList);
            } 
            return reslut;
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeAdd)]
        public async Task<int> Create(EmployeeEdit input)
        {
            var entity = input.MapTo<Employee>();
            long userid = 0;
            var companyId = 0;

            var isExist=await  _ERepository.CountAsync(p => p.FMpno == input.FMpno&&p.IsDeleted==false);

            if (isExist > 0)
            {
                throw new UserFriendlyException($"员工编号：{input.FMpno}已存在");
            }

            //指定默认邮箱
            //input.FEmailAddress = string.IsNullOrEmpty(input.FEmailAddress) ? $"test{count}@jit.com" : input.FEmailAddress;

            if (input.FSystemUser == 1)
            {
               var CreateUserDto = input.User;

               var UserDto = new CreateUserDto
               {
                   UserName = string.IsNullOrEmpty(CreateUserDto.UserName)?input.FMpno: CreateUserDto.UserName,
                   Name = input.FName,
                   Surname = input.FName,
                   EmailAddress = input.FEmailAddress,
                   IsActive=true,
                   RoleNames= null, 
                   //默认用户密码
                   Password = string.IsNullOrEmpty( CreateUserDto.Password)?User.DefaultPassword:CreateUserDto.Password
               };

                #region 旧代码

                //var  Users =new User()
                // {
                //     UserName = UserDto.UserName,
                //     EmailAddress = input.FEmailAddress,
                //     Name = input.FName,
                //     Surname = input.FName,
                //     Password = UserDto.Password,
                //     IsActive = true,
                //     IsDeleted = false,
                //     NormalizedUserName = input.FName,
                //     NormalizedEmailAddress = input.FEmailAddress,            
                //     IsTwoFactorEnabled = false,
                //     IsPhoneNumberConfirmed = false,
                //     IsEmailConfirmed = true,
                //     AccessFailedCount = 0,
                //     IsLockoutEnabled = false,
                //     CreationTime = DateTime.Now,
                // };
                // userid = _UserRepository.InsertAndGetId(Users);

                #endregion
                userid = UserAppService.Create(UserDto).Result.Id;

                //User u = UserDto.MapTo<User>();
                //u.NormalizedUserName = u.UserName.ToUpper();
                //u.Password = PasswordHasher.HashPassword(u, u.Password);
                //userid = await _UserRepository.InsertAndGetIdAsync(u);

            }
            if (input.FDepartment > 0) //部门ID
            {

                var treelist = _Repository.GetAll().Where(p => p.IsDeleted == false).ToList();

                //查找部门的那条数据
                var eneiety = _Repository.GetAll().Include(p => p.Parent)
                    .SingleOrDefault(p => p.Id == input.FDepartment && p.IsDeleted == false);
                if (eneiety != null)
                {
                   var  resultjt = GetCompany(eneiety, treelist);
                    companyId = resultjt.Id;
                }         
            }

            entity.FOrganizationUnitId = companyId;
            entity.FTenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0;      
            entity.FUserId = userid;
            entity.IsDeleted = false;

           // return 0;
            return await _ERepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffUpdate)]
        public async Task<Employee> Update(EmployeeEdit input)
        {
            if (!string.IsNullOrEmpty(input.FIDCard))
            {
                Employee emp;
                if (!checkFIDCard(input, out emp))
                {
                    throw  new UserFriendlyException($"【{input.FIDCard}】此ID卡已绑定到员工【{emp.FName}】，不能重复绑定,请换卡！");
                }
            }

            var entity = input.MapTo<Employee>();
            long userid;

            //修改员工对应用户信息
            CreateOrUpdateUser(input,out userid);

            if (input.FDepartment > 0) //部门ID
            {

                var treelist = _Repository.GetAll().Where(p => p.IsDeleted == false).ToList();

                //查找部门的那条数据
                var eneiety = _Repository.GetAll().Include(p => p.Parent)
                    .SingleOrDefault(p => p.Id == input.FDepartment && p.IsDeleted == false);
                if (eneiety != null)
                {
                    var resultjt = GetCompany(eneiety, treelist);
                    entity.FOrganizationUnitId = resultjt.Id;
                }
            }

            entity.FTenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0;
            entity.FERPUser = input.FERPUser;
            entity.FERPOfficeClerk = input.FERPOfficeClerk;
            entity.FUserId =(userid!=0&& input.FUserId==0)?userid:input.FUserId;
            entity.IsDeleted = false;
            entity.FHiredate = (entity.FHiredate == null ||entity.FHiredate==DateTime.MinValue)? DateTime.Now:entity.FHiredate;
            var count= await _ERepository.UpdateAsync(entity);
            return count;
        }


        private  bool checkFIDCard(EmployeeEdit input,out Employee employee)
        {
            employee =  _ERepository.GetAll().SingleOrDefault(p => p.FIDCard == input.FIDCard&&p.Id!=input.Id);
            if (employee != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 创建或更新员工对应的用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_OrganizeUpdate, ProductionPlanPermissionsNames.BasicData_StaffAdd)]
        private  void CreateOrUpdateUser(EmployeeEdit input,out long userid)
        {
            userid = 0;
            if (input.FSystemUser == 1) //是系统用户
            {
                if (input.FUserId == 0)
                {
                    var CreateUserDto = input.User;
                    var UserDto = new CreateUserDto
                    {
                        UserName = string.IsNullOrEmpty(CreateUserDto.UserName)? input.FMpno : CreateUserDto.UserName,
                        Name = input.FName,
                        Surname = input.FName,
                        EmailAddress = input.FEmailAddress,
                        IsActive = true,
                        RoleNames = null,
                        Password =string.IsNullOrEmpty( CreateUserDto.Password)?User.DefaultPassword:CreateUserDto.Password
                    };

                    userid = UserAppService.Create(UserDto).Result.Id;
                }
                else
                {
                    //不加查询条件去返回对象来更改会报错
                    User entitys = null;
                    
                        entitys =  _UserRepository.GetAll()
                            .SingleOrDefault(p => p.Id == input.FUserId);
                   
                    if (entitys != null)
                    {
                        entitys.IsActive = true;
                         _UserRepository.Update(entitys);
                    }
                }
            }
            else
            {
                //执行移除的操作  只改变原有的启用状态否
                if (input.FUserId > 0)
                {
                    //不加查询条件去返回对象来更改会报错
                    var entitys =  _UserRepository.GetAll()
                        .SingleOrDefault(p => p.Id == input.FUserId);
                    if (entitys != null)
                    {
                        entitys.IsActive = false;
                         _UserRepository.Update(entitys);
                    }
                }
            }

            //修改为不在职状态的情况下
            if (input.FWorkingState == 2)
            {
                if (input.FUserId > 0)
                {
                    //不加查询条件去返回对象来更改会报错
                    var entitys =  _UserRepository.GetAll()
                        .SingleOrDefault(p => p.Id == input.FUserId);
                    if (entitys != null)
                    {
                        entitys.IsActive = false;
                         _UserRepository.Update(entitys);
                    }
                }
            }
            else
            {
                //在职状态的情况下  不是系统用户 
                if (input.FSystemUser == 2)
                {
                    if (input.FUserId > 0)
                    {
                        //不加查询条件去返回对象来更改会报错
                        var entitys =  _UserRepository.GetAll()
                            .SingleOrDefault(p => p.Id == input.FUserId);
                        if (entitys != null)
                        {
                            entitys.IsActive = false;
                            //entity.IsDeleted = false;
                             _UserRepository.Update(entitys);
                        }
                    }
                }
                else
                {
                    if (input.FUserId > 0)
                    {
                        //不加查询条件去返回对象来更改会报错
                        var entitys =  _UserRepository.GetAll()
                            .SingleOrDefault(p => p.Id == input.FUserId);
                        if (entitys != null)
                        {
                            entitys.IsActive = true;
                            //entity.IsDeleted = false;
                             _UserRepository.Update(entitys);
                        }
                    }
                }
            }
        }

        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffDelete)]
        public async Task<int> Delete(EmployeeDelete input)
        {
            try
            {
                var entity =
                    await _ERepository.GetAll().SingleOrDefaultAsync(p =>
                        p.Id == input.Id&&p.IsDeleted==false);

                if (entity.FUserId!=0)
                {
                    await _UserRepository.DeleteAsync(entity.FUserId);

                }

               entity.IsDeleted = true;
               await _ERepository.UpdateAsync(entity);
               return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffGet)]
        public async Task<PagedResultDto<VWEmployeesDto>> GetAllVW(VWEmployeesGetAllInputDto input)
        {

            if (input.Id==0)
            {
                var querys = _VwRepository.GetAll().Where(p=>!p.IsDeleted).Where(input.Where);
                var count = await querys.CountAsync();      
                var data = await querys.OrderBy(p => p.Id).Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToListAsync();
                var list = data.MapTo<List<VWEmployeesDto>>();
                return new PagedResultDto<VWEmployeesDto>(count, list);
            }
            else
            {

                var Querybufid = _Repository.GetAll().SingleOrDefault(p => p.Id == input.Id);
                IQueryable<VW_Employee> querys;
                int count = 0;
                List<VW_Employee> data = new EditableList<VW_Employee>();
                List<VWEmployeesDto> list=new List<VWEmployeesDto>();

                if (Querybufid.ParentId==0&& Querybufid.OrganizationType==PublicEnum.OrganizationType.集团)
                { 

                    int[] FDepartmentIDArr = GetOneselfAndJunior(new int[] { input.Id });

                    querys = _VwRepository.GetAll().Where(p=>!p.IsDeleted).Where(input.Where).Where(w => FDepartmentIDArr.Contains(w.FDepartment));

                    count = await _ERepository.GetAll().CountAsync();
                    data = await querys.OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    list = data.MapTo<List<VWEmployeesDto>>();

                }
                else if (Querybufid.OrganizationType== PublicEnum.OrganizationType.公司)
                {
                    querys = _VwRepository.GetAll().Where(input.Where).Where(p => p.IsDeleted == false && p.FOrganizationUnitId == input.Id);
                    count = await querys.CountAsync();
                    data = await querys.OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    list = data.MapTo<List<VWEmployeesDto>>();
                }
                else if(Querybufid.OrganizationType==PublicEnum.OrganizationType.部门)
                {

                    int[] FDepartmentIDArr = GetOneselfAndJunior(new int[] {input.Id});

                    querys = _VwRepository.GetAll().Where(p => p.IsDeleted == false && FDepartmentIDArr.Contains(p.FDepartment)).Where(input.Where);

                    count = await querys.CountAsync();
                    data = await querys.OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    list = data.MapTo<List<VWEmployeesDto>>();
                }


                return new PagedResultDto<VWEmployeesDto>(count, list);
            }
        }

        /// <summary>
        /// 取自己以及下级部门所有人
        /// </summary>
        /// <param name="ArrParentID"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffGet)]
        public int[] GetOneselfAndJunior(int[] ArrParentID)
        {
            try
            {
                //if (ArrParentID.Length == 0)
                //{
                //    return ArrParentID;
                //}
                int[] ArrJunior = _Repository.GetAll().Where(p => p.IsDeleted == false && ArrParentID.Contains(p.ParentId))
                    .Select(s => s.Id).ToArray();
                if (ArrJunior.Length == 0)
                {
                    return ArrParentID;
                }

                return ArrJunior.Union(GetOneselfAndJunior(ArrJunior)).Union(ArrParentID).ToArray();
            }
            catch (Exception e)
            {           
                Console.Write(e.Message);
                return ArrParentID;
            }
             
        }

        /// <summary>
        /// 返回拼接的节点信息 上级主管
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>

        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffGet)]
        public async Task<List<OrganizationDtoTest>> GetTreeList(int ParentID)
        {
            List<OrganizationDtoTest> TreeList = new List<OrganizationDtoTest>();
           
            var quers = _ERepository.GetAll().Where(p => p.IsDeleted == false).ToList();
            var result = quers.Where(x => x.FParentId == ParentID);       
            foreach (var item in result.ToList())
            {
                OrganizationDtoTest m = new OrganizationDtoTest();
                m.Id = item.Id;    
                m.ParentId = item.FParentId;
                m.value = item.Id.ToString();
                m.label = item.FName;
                m.children = await GetTreeList(int.Parse(item.Id.ToString()));
                TreeList.Add(m);
            }
            return TreeList;
        }

        /// <summary>
        /// 返回所有的车间员工
        /// </summary>
        /// <returns></returns>

        [AbpAuthorize(ProductionPlanPermissionsNames.BasicData_StaffGet)]
        public async Task<PagedResultDto<EmployeeDto>> GetAllWorkers(JITPagedResultRequestDto input)
        {
            var query = _ERepository.GetAll().Where(input.Where).Include(p => p.Department)
                .Where(p => p.Department.FWorkshopType == true&&p.IsDeleted==false);




            var count = await query.CountAsync();

            var data =await query.OrderBy(p=>p.FName).PageBy(input).ToListAsync();

            var list = data.MapTo<List<EmployeeDto>>();

            return new  PagedResultDto<EmployeeDto>(count,list);
        }


    }
}
