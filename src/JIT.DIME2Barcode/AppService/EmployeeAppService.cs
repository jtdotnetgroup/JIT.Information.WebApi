using Abp.Application.Services;
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
using Abp;
using Abp.UI;
using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Identity;

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
        protected t_OrganizationUnit GetCompany(t_OrganizationUnit node,List<t_OrganizationUnit> terrList)
        {
            t_OrganizationUnit reslut = null;
            if (node.OrganizationType == PublicEnum.OrganizationType.公司|| node.OrganizationType==PublicEnum.OrganizationType.集团)
            {
                return node;
            }

            if (node.ParentId!=null)
            {
                var parent = terrList.SingleOrDefault(p=>p.Id== node.ParentId);
                 reslut = GetCompany(parent,terrList);
            }

            return reslut;
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Create(EmployeeEdit input)
        {
            var entity = input.MapTo<Employee>();
            long userid = 0;
            var companyId = 0;

            var isExist=await  _ERepository.CountAsync(p => p.FMpno == input.FMpno);

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
        public async Task<Employee> Update(EmployeeEdit input)
        {
            var entity = input.MapTo<Employee>();
            long userid = 0;

            //修改员工对应用户信息
            await CreateOrUpdateUser(input);

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
            entity.FERPUser = input.FERPUser == null ? 0 : input.FERPUser;
            entity.FERPOfficeClerk = input.FERPOfficeClerk == null ? 0 : input.FERPOfficeClerk;
            entity.FUserId = userid==0?input.FUserId: userid;
            entity.IsDeleted = false;
            var count= await _ERepository.UpdateAsync(entity);
            return count;
        }

        /// <summary>
        /// 创建或更新员工对应的用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CreateOrUpdateUser(EmployeeEdit input)
        {
            long userid;
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
                    try
                    {
                        entitys = await _UserRepository.GetAll()
                            .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }


                    Console.WriteLine(entitys);
                    if (entitys != null)
                    {
                        entitys.IsActive = true;
                        await _UserRepository.UpdateAsync(entitys);
                    }
                }
            }
            else
            {
                //执行移除的操作  只改变原有的启用状态否
                if (input.FUserId > 0)
                {
                    //不加查询条件去返回对象来更改会报错
                    var entitys = await _UserRepository.GetAll()
                        .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                    if (entitys != null)
                    {
                        entitys.IsActive = false;
                        await _UserRepository.UpdateAsync(entitys);
                    }
                }
            }

            //修改为不在职状态的情况下
            if (input.FWorkingState == 2)
            {
                if (input.FUserId > 0)
                {
                    //不加查询条件去返回对象来更改会报错
                    var entitys = await _UserRepository.GetAll()
                        .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                    if (entitys != null)
                    {
                        entitys.IsActive = false;
                        await _UserRepository.UpdateAsync(entitys);
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
                        var entitys = await _UserRepository.GetAll()
                            .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                        if (entitys != null)
                        {
                            entitys.IsActive = false;
                            //entity.IsDeleted = false;
                            await _UserRepository.UpdateAsync(entitys);
                        }
                    }
                }
                else
                {
                    if (input.FUserId > 0)
                    {
                        //不加查询条件去返回对象来更改会报错
                        var entitys = await _UserRepository.GetAll()
                            .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                        if (entitys != null)
                        {
                            entitys.IsActive = true;
                            //entity.IsDeleted = false;
                            await _UserRepository.UpdateAsync(entitys);
                        }
                    }
                }
            }
        }


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
                return 0;
                Console.WriteLine(e);
              
            }

        }

        public async Task<PagedResultDto<VWEmployeesDto>> GetAllVW(VWEmployeesGetAllInputDto input)
        {

            if (input.Id==0)
            {
                var querys = _VwRepository.GetAll().Where(p => p.IsDeleted == false);
                var count = await _ERepository.GetAll().CountAsync(p => p.IsDeleted==false );      
                var data = await querys.OrderBy(p => p.Id).Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToListAsync();
                var list = data.MapTo<List<VWEmployeesDto>>();
                return new PagedResultDto<VWEmployeesDto>(count, list);
            }
            else
            {

                var Querybufid = _Repository.GetAll().SingleOrDefault(p => p.Id == input.Id);
                IQueryable<VW_Employee> querys;
                int count = 0;
                List<VW_Employee> data=new EditableList<VW_Employee>();
                List<VWEmployeesDto> list=new List<VWEmployeesDto>();

                if (Querybufid.ParentId==0&& Querybufid.OrganizationType==PublicEnum.OrganizationType.集团)
                {
                    //querys = _VwRepository.GetAll().Where(p => p.IsDeleted == false);
                    //count = await _ERepository.GetAll().CountAsync(p => p.IsDeleted == false);
                    //data = await querys.OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    //list = data.MapTo<List<VWEmployeesDto>>();


                    int[] FDepartmentIDArr = GetOneselfAndJunior(new int[] { input.Id });

                    querys = from a in _VwRepository.GetAll()
                        where (FDepartmentIDArr).Contains(a.FDepartment)
                        select a;

                    count = await _ERepository.GetAll().CountAsync(p =>
                        p.IsDeleted == false && FDepartmentIDArr.Contains(p.FDepartment));
                    data = await querys.Where(p => p.IsDeleted == false).OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    list = data.MapTo<List<VWEmployeesDto>>();

                }
                else if (Querybufid.OrganizationType== PublicEnum.OrganizationType.公司)
                {
                    querys = _VwRepository.GetAll().Where(p => p.IsDeleted == false && p.FOrganizationUnitId == input.Id);
                    count = await _ERepository.GetAll().CountAsync(p => p.IsDeleted == false && p.FOrganizationUnitId == input.Id);
                    data = await querys.OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
                    list = data.MapTo<List<VWEmployeesDto>>();
                }
                else if(Querybufid.OrganizationType==PublicEnum.OrganizationType.部门)
                {

                    int[] FDepartmentIDArr = GetOneselfAndJunior(new int[] {input.Id});

                    querys = from a in _VwRepository.GetAll()
                             where (FDepartmentIDArr).Contains(a.FDepartment)
                             select a;

                    count = await _ERepository.GetAll().CountAsync(p =>
                        p.IsDeleted == false && FDepartmentIDArr.Contains(p.FDepartment));
                    data = await querys.Where(p=>p.IsDeleted==false).OrderBy(p => p.Id).Skip(input.MaxResultCount * (input.SkipCount)).Take(input.MaxResultCount).ToListAsync();
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
                return ArrParentID;
            }
             
        }


        /// <summary>
        /// 员工编号
        /// </summary>
        /// <returns></returns>
        public async Task<string> FMpno()
        {
            // var FMpno = "";    
            // //查询最后一条没有别删除的编号
            // var enetity = _ERepository.GetAll().LastOrDefault();
            // if (enetity!=null)
            // {
            //     //如果存在这个编号就在原来基础上增加
            //     string[] strFMpno = enetity.FMpno.Split("YK");
            //     FMpno = "YK0000" + (Convert.ToInt32(strFMpno[1]) + 1);

            //     var quers = _ERepository.GetAll().SingleOrDefault(p => p.FMpno == FMpno);
            //     if (quers!=null)
            //     {
            //         FMpno = "YK0000" + (Convert.ToInt32(strFMpno[1]) + 2);
            //     }
            // }
            // else
            // {
            //     FMpno = "YK00001";
            // }

            // return FMpno;

            //var FMpno = "";    
            ////查询最后一条没有别删除的编号
            //var enetity = _ERepository.GetAll().LastOrDefault();
            //if (enetity!=null)
            //{
            //    //如果存在这个编号就在原来基础上增加
            //    string[] strFMpno = enetity.FMpno.Split("YK");
            //    FMpno = "YK0000" + (Convert.ToInt32(strFMpno[1]) + 1);

            //    var quers = _ERepository.GetAll().SingleOrDefault(p => p.FMpno == FMpno);
            //    if (quers!=null)
            //    {
            //        FMpno = "YK0000" + (Convert.ToInt32(strFMpno[1]) + 2);
            //    }
            //}
            //else
            //{
            //    FMpno = "YK00001";
            //}

            //return FMpno;
            return "";
        }

        /// <summary>
        /// 返回拼接的节点信息 上级主管
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
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
        public async Task<PagedResultDto<EmployeeDto>> GetAllWorkers()
        {
            var query = _ERepository.GetAllIncluding(p => p.Department)
                .Where(p => p.Department.FWorkshopType == true&&p.IsDeleted==false);

            var count = await query.CountAsync();

            var data =await query.ToListAsync();

            var list = data.MapTo<List<EmployeeDto>>();

            return new  PagedResultDto<EmployeeDto>(count,list);
        }


    }
}
