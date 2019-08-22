using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Permissions;
using JIT.DIME2Barcode.TaskAssignment.Printing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 打印
    /// </summary>
    public class PrintingAppService : BaseAppService
    {

        public IRepository<vw_PrintLabel> PrintLabelRepository { get; set; }

        /// <summary>
        /// 打印质检单
        /// </summary>
        /// <param name="FID">所有质检单的FID</param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize(ProductionPlanPermissionsNames.TouchPadBarCode)]
        public async Task<List<vw_PrintLabel>> GetAllPrinting(PrintingInput input)
        {
            var query =await PrintLabelRepository.GetAll().Where(w => input.FID.Contains(w.FID)).ToListAsync();
            
            return query;
        }
    }
}
