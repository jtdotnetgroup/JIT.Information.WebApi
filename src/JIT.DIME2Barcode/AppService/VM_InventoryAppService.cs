﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.TaskAssignment.VM_Inventory.Dtos;
using JIT.JIT.TaskAssignment.VW_MODispBillList.Dtos;

namespace JIT.DIME2Barcode.AppService
{
    public class VM_InventoryAppService : ApplicationService
    {
        public IRepository<DIME2Barcode.Entities.VM_Inventory,int> Repository { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public PagedResultDto<VM_Inventory> GetAll(VM_InventoryGetAllInput input)
        {
            var query = Repository.GetAll();
            var data = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var list = data.MapTo<List<VM_Inventory>>();
            return new PagedResultDto<VM_Inventory>(query.Count(), list);
        }
    }
}
