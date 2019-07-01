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
using Abp;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using Abp.UI;
using JIT.DIME2Barcode.BaseData.Equipment.ISpecification;
using JIT.DIME2Barcode.Permissions;

namespace JIT.DIME2Barcode.AppService
{
   
    public class EquipmentAppService:ApplicationService
    {
        private IRepository<Equipment,int> _repository { get; set; }
        //设备班次仓储
        public IRepository<EqiupmentShift,int> EsRepository { get; set; }

        public IRepository<t_OrganizationUnit> ORepository { get;set; }

        public EquipmentAppService(IRepository<Equipment, int> repository)
        {
            _repository = repository;
        }
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Create)]
        public async Task<EquipmentDto> Create(EquipmentDto input)
        {
            var entity = input.MapTo<Equipment>();

            var count =await  _repository.CountAsync(p => p.FNumber == input.FNumber);

            if (count > 0)
            {
                throw new UserFriendlyException($"设备代码{input.FNumber}重复");
            }

            entity =await  _repository.InsertAsync(entity);

            return entity.MapTo<EquipmentDto>();
        }
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            var entity = await _repository.GetAsync(input.Id);

            await _repository.DeleteAsync(entity);
        }
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Update)]
        public async Task<EquipmentDto> Update(EquipmentDto input)
        {
            var entity = input.MapTo<Equipment>();

            entity = await _repository.UpdateAsync(entity);

            return entity.MapTo<EquipmentDto>();
        }
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Get)]
        public async Task<PagedResultDto<EquipmentDto>> GetAll(EquipmentGetAllInput input)
        {
            var query = _repository.GetAll();

            if (input.OrganizationID.HasValue)
            {
                var eos = new EquipmentOrgainzationSpecification(input.OrganizationID.Value);
                query=query.Where(eos);
            }

            if (!string.IsNullOrEmpty(input.OrganizationCode))
            {
                var org = await ORepository.GetAll().Where(p => p.Code == input.OrganizationCode).SingleOrDefaultAsync();
                query = query.Where(p => p.FWorkCenterID == org.Id);
            }

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
        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Create, ProductionPlanPermissionsNames.TaskDispatch_Update)]
        public async Task<int> CreateOrUpdateShift(List<EquipmentShiftDto> input)
        {
            int count = 0;
            foreach (var dto in input)
            {
                EqiupmentShift entity = dto.MapTo<EqiupmentShift>();
                if (entity.Id != 0)
                {
                    await EsRepository.UpdateAsync(entity);
                }
                else
                {
                    await EsRepository.InsertAsync(entity);
                }

                count++;
            }

            return count;
        }

        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Get)]
        public async Task DeleteShift(EntityDto input)
        {
            var entity = await EsRepository.GetAsync(input.Id);

            if (entity != null)
            {
                await EsRepository.DeleteAsync(entity);
            }
        }

        [AbpAuthorize(ProductionPlanPermissionsNames.TaskDispatch_Get)]
        public async Task<List<EquipmentShiftDto>> GetShiftByEquipmentID(int Id)
        {
            var query = EsRepository.GetAll().Where(p => p.FEqiupmentID == Id).Include(p => p.Employee)
                .Include(p => p.Equipment);

            var list =await query.ToListAsync();

            return list.MapTo<List<EquipmentShiftDto>>();
        }
    }
}