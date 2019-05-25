using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Employee.Dtos;
using JIT.InformationSystem.Authorization.Roles;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.Users;
using JIT.InformationSystem.Users.Dto;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    public class EmployeeAppService : ApplicationService
    {
        public IRepository<Employee, int> _ERepository { get; set; }

        public IRepository<User, long> _UserRepository { get; set; }

        public IRepository<Role, int> _UserRoleRepository { get; set; }

        public IRepository<OrganizationUnitsJT, int> _Repository { get; set; }

        public IUserAppService UserAppService { get; set; }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Create(EmployeeEdit input)
        {
            var entity = input.MapTo<Employee>();
            long userid = 0;
            if (input.FSystemUser == 1)
            {
               var  User= input.User;

               var UserDto = new CreateUserDto
               {
                   UserName = User.UserName,
                   Name = input.FName,
                   Surname = input.FName,
                   EmailAddress = input.FEmailAddress,
                   IsActive=false,
                   RoleNames= null, 
                   Password = User.Password
               };
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

                userid = UserAppService.Create(UserDto).Result.Id;
     
            }

            entity.FTenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0;
            entity.FOrganizationUnitId = input.FParentId;
            entity.FUserId = userid;
            entity.IsDeleted = false;
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
            if (input.FSystemUser == 1)//是系统用户
            {
                if (input.FUserId == 0)
                {
                    var User = input.User;
                    var UserDto = new CreateUserDto
                    {
                        UserName = User.UserName,
                        Name = input.FName,
                        Surname = input.FName,
                        EmailAddress = input.FEmailAddress,
                        IsActive = false,
                        RoleNames = null,
                        Password = User.Password
                    };
                    userid = UserAppService.Create(UserDto).Id;

                }            
                //var entitys = await _UserRepository.GetAll()
                //    .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                //if (entitys!=null)
                //{
                //    entitys.UserName = input.FName;
                //    entitys.EmailAddress = input.FEmailAddress;
                //    entitys.Name = input.FName;
                //    entitys.Surname = input.FName;
                //    entitys.Password = User.DefaultPassword;
                //    entitys.NormalizedUserName = input.FName;
                //    entitys.NormalizedEmailAddress = input.FEmailAddress;
                //    entitys.LastModificationTime = DateTime.Now;
                //    await _UserRepository.UpdateAsync(entitys);
                //}            
            }
            else
            {
                //执行移除的操作
                if (input.FUserId > 0)
                {             
                //不加查询条件去返回对象来更改会报错
                    var entitys = await _UserRepository.GetAll()
                    .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                if (entitys != null)
                {
                    entitys.IsDeleted = true;
                    entitys.LastModificationTime = DateTime.Now;
                    await _UserRepository.UpdateAsync(entitys);
                }
               }
            }

            entity.FTenantId = this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0;
            entity.FOrganizationUnitId = input.FParentId;
            entity.FUserId = userid;
            entity.IsDeleted = false;
            var count= await _ERepository.UpdateAsync(entity);
            return count;
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
                    var entitys = await _UserRepository.GetAll()
                        .SingleOrDefaultAsync(p => p.Id == entity.FUserId && p.IsDeleted == false);
                    if (entitys != null)
                    {
                        entitys.IsDeleted = true;
                        entitys.LastModificationTime = DateTime.Now;
                        await _UserRepository.UpdateAsync(entitys);
                    }
                   
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

        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<EmployeeDto>> GetAll(EmployeeGetAll input)
        {

           // var query = _ERepository.GetAll().Where(p=>p.IsDeleted == false).OrderBy(p => p.Id).PageBy(input);

          
            var querys = from a in _ERepository.GetAll()
                   join o in _Repository.GetAll() on a.FOrganizationUnitId equals o.Id
                                    
                         select new
                         {
                             a.Id,
                             a.FMpno,
                             a.FName,
                             a.FSex,
                             a.FDepartment,//部门
                             a.FWorkingState,
                             a.FSystemUser,
                             a.FParentId,
                             a.FPhone,
                             a.FHiredate,
                             a.FEmailAddress,
                             a.FERPUser,
                             a.FERPOfficeClerk,
                             a.FTenantId,
                             a.FOrganizationUnitId,
                             a.FUserId,
                             a.IsDeleted,
                             o.DisplayName,
                             //u.UserName,
                             //u.Password,
                         };

            

            var count = await _ERepository.GetAll().CountAsync();
            var data = await querys.Where(p => p.IsDeleted == false&&p.FOrganizationUnitId==input.Id).OrderBy(p => p.Id).PageBy(input).ToListAsync();
            var list = data.MapTo<List<EmployeeDto>>();
            return new PagedResultDto<EmployeeDto>(count, list);



        }


        /// <summary>
        /// 员工编号
        /// </summary>
        /// <returns></returns>
        public async Task<string> FMpno()
        {

            var FMpno = "";
            var enetity = _ERepository.Count(p=>p.IsDeleted==false);
            FMpno= "YK0000" + (enetity + 1);
            var querys = _ERepository.GetAll().SingleOrDefault(p => p.FMpno == FMpno);
            if (querys!=null)
            {
                FMpno = "YK0000" + (enetity + 2);
            }
            return FMpno;
        }
    }
}
