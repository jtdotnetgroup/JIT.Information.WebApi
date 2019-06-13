using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseAppService: ApplicationService
    {
        public IRepository<DIME2Barcode.Entities.VW_MODispBillList, string> JIT_VW_MODispBillList { get; set; }
        public IRepository<DIME2Barcode.Entities.VM_Inventory, int> JIT_VM_Inventory { get; set; }
        public IRepository<DIME2Barcode.Entities.ICException, string> JIT_ICException { get; set; }
        public IRepository<DIME2Barcode.Entities.ICMODispBill, string> JIT_ICMODispBill { get; set; }
        public IRepository<DIME2Barcode.Entities.ICMODispBillRecord, string> JIT_ICMODispBillRecord { get; set; }
        
        public IRepository<Employee, int> JIT_Employee { get; set; }

    }
}
