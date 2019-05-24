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

using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    public class EmployeeAppService : ApplicationService
    {
        public IRepository<Employee, int> _ERepository { get; set; }

        public IRepository<User, long> _UserRepository { get; set; }

        public IRepository<Role, int> _UserRoleRepository { get; set; }

        public IRepository<OrganizationUnitsJT, int> _Repository { get; set; }

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
               var  UserDto = input.User;      
               var  User =new User()
                {
                    UserName = UserDto.UserName,
                    EmailAddress = UserDto.EmailAddress,
                    Name = UserDto.UserName,
                    Surname = UserDto.UserName,
                    Password = UserDto.Password,
                    IsActive = true,
                    IsDeleted = false,
                    NormalizedUserName = UserDto.UserName,
                    NormalizedEmailAddress = UserDto.EmailAddress,            
                    IsTwoFactorEnabled = false,
                    IsPhoneNumberConfirmed = false,
                    IsEmailConfirmed = true,
                    AccessFailedCount = 0,
                    IsLockoutEnabled = false,
                    CreationTime = DateTime.Now,
                };
                userid = _UserRepository.InsertAndGetId(User);
            }
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

            if (input.FSystemUser == 1)
            {
                var UserDto = input.User;
                var entitys = await _UserRepository.GetAll()
                    .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                entitys.UserName = UserDto.UserName;
                entitys.EmailAddress = UserDto.EmailAddress;
                entitys.Name = UserDto.UserName;
                entitys.Surname = UserDto.UserName;
                entitys.Password = UserDto.Password;               
                entitys.NormalizedUserName = UserDto.UserName;
                entitys.NormalizedEmailAddress = UserDto.EmailAddress;              
                entitys.LastModificationTime = DateTime.Now;
                await _UserRepository.UpdateAsync(entitys); 
            }
            else
            {
                //不加查询条件去返回对象来更改会报错
                var entitys = await _UserRepository.GetAll()
                    .SingleOrDefaultAsync(p => p.Id == input.FUserId);
                entitys.IsDeleted = true;
                entitys.LastModificationTime = DateTime.Now;
                await _UserRepository.UpdateAsync(entitys);
            }
            
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
                    entitys.IsDeleted = true;
                    entitys.LastModificationTime = DateTime.Now;
                    await _UserRepository.UpdateAsync(entitys);
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
                         };

            

            var count = await _ERepository.GetAll().CountAsync();
            var data = await querys.Where(p => p.IsDeleted == false&&p.FOrganizationUnitId==input.Id).OrderBy(p => p.Id).PageBy(input).ToListAsync();
            var list = data.MapTo<List<EmployeeDto>>();
            return new PagedResultDto<EmployeeDto>(count, list);

        }

    }
}
