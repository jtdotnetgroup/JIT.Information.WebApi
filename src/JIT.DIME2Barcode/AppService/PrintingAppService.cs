using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIT.DIME2Barcode.TaskAssignment.Printing;
using Microsoft.AspNetCore.Mvc;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 打印
    /// </summary>
    public class PrintingAppService : BaseAppService
    {
        /// <summary>
        /// 打印质检单
        /// </summary>
        /// <param name="FID">所有质检单的FID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Printing>> GetAllPrinting(PrintingInput input)
        { 
            var data = JIT_VW_MODispBillList.GetAll().Join(JIT_ICMOInspectBill.GetAll().Where(w => input.FID.Contains(w.FID)),
                A => A.FID, B => B.ICMODispBillID, (A, B) => new Printing
                {
                    ItemNum = A.产品代码,
                    ItemName = A.产品名称,
                    PackQty = A.F_102,
                    LotNum = B.BatchNum,
                    Qty = B.FPassAuxQty,
                    QRCode = ""
                }).ToList();
            return data;
        }
    }
}
