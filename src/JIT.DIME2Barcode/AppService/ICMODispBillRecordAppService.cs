using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace JIT.DIME2Barcode.AppService
{
    public class ICMODispBillRecordAppService: BaseAppService
    {
        /// <summary>
        /// 派工任务单记录创建
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public async Task<bool> Create(string FID)
        {
            try
            {
                // 查询回原来的任务单号
                var tmp = await JIT_ICMODispBill.SingleAsync(s => s.FID == FID); 
                // 
                Entities.ICMODispBillRecord icmoDispBillRecord = tmp.MapTo<Entities.ICMODispBillRecord>();
                icmoDispBillRecord.FID = Guid.NewGuid().ToString();
                icmoDispBillRecord.FSrcID = tmp.FID;
                icmoDispBillRecord.CreateTime=DateTime.Now;
                icmoDispBillRecord.CreateTime = DateTime.Now;
                await JIT_ICMODispBillRecord.InsertAsync(icmoDispBillRecord);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
