using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.SystemSetting.Employee.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
   public class EmployeeAppService: ApplicationService
    {
        public IRepository<Employee, int> _ERepository { get; set; }


        public async Task<int> Create(EmployeeEdit input)
        {

            var entity = input.MapTo<Employee>();
            //var entity = new Employee
            //{
            //    FMpno = input.FMpno
            //};

            return  await _ERepository.InsertAndGetIdAsync(entity);

        }

        public async Task<PagedResultDto<EmployeeDto>> GetAll(EmployeeGetAll input)
        {
          
            var query = _ERepository.GetAll().OrderBy(p => p.Id).PageBy(input);

            var count = await _ERepository.GetAll().CountAsync();

            var data = await query.ToListAsync();

            var list = data.MapTo<List<EmployeeDto>>();

            return new PagedResultDto<EmployeeDto>(count, list);

        }


    }
}
