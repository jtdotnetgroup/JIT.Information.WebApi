using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.BaseData.Equipment.Dtos;
using JIT.DIME2Barcode.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace JIT.DIME2Barcode.AppService
{
    public class EquipmentAppService:ApplicationService
    {
        private IRepository<Equipment,int> _repository { get; set; }
        public EquipmentAppService(IRepository<Equipment, int> repository)
        {
            _repository = repository;
        }

        public async Task<EquipmentDto> Create(EquipmentDto input)
        {
            var entity = input.MapTo<Equipment>();

            entity =await  _repository.InsertAsync(entity);

            return entity.MapTo<EquipmentDto>();
        }

        public async Task Delete(EntityDto<int> input)
        {
            var entity = await _repository.GetAsync(input.Id);

            await _repository.DeleteAsync(entity);
        }

        public async Task<EquipmentDto> Update(EquipmentDto input)
        {
            var entity = input.MapTo<Equipment>();

            entity = await _repository.UpdateAsync(entity);

            return entity.MapTo<EquipmentDto>();
        }

        public async Task<PagedResultDto<EquipmentDto>> GetAll(EquipmentGetAllInput input)
        {
            var query = _repository.GetAll();

            var count = await query.CountAsync();

            input.Sorting = string.IsNullOrEmpty(input.Sorting) ? "FInterID" : input.Sorting;

            query = query.OrderBy(input.Sorting).PageBy(input).Include(o=>o.WorkCenter);

            var data = await query.ToListAsync();

            var list = new List<EquipmentDto>();

            foreach (var e in data)
            {
                var item = e.MapTo<EquipmentDto>();
                list.Add(item);
            }

            return new PagedResultDto<EquipmentDto>(count,list);
        }
    }
}