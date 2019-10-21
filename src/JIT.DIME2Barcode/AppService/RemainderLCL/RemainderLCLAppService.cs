using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using JIT.DIME2Barcode.TaskAssignment.RemainderLCL;
using Microsoft.EntityFrameworkCore; 

namespace JIT.DIME2Barcode.AppService.RemainderLCL
{
    /// <summary>
    /// 余数拼箱
    /// </summary>
    public class RemainderLCLAppService : BaseAppService
    {
        /// <summary>
        /// 余数拼箱
        /// </summary>
        public async Task<PagedResultDto<Entities.RemainderLCL>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var list = await JIT_RemainderLCL.GetAll().PageBy(input).ToListAsync();
            return new PagedResultDto<Entities.RemainderLCL>(JIT_RemainderLCL.GetAll().Count(), list); 
        }

        /// <summary>
        /// 创建
        /// </summary>
        public void Create(List<RemainderLCLCreateInput> mCreateObjs)
        {
            try
            {
                // 
                foreach (var item in mCreateObjs)
                {
                    Entities.RemainderLCL m = new Entities.RemainderLCL();
                    m.LCLId = Guid.NewGuid().ToString();
                    m.LCLCode = "PDH" + m.LCLId;
                    m.LCLTime = DateTime.Now;
                    m.CreateUserId = AbpSession.UserId.ToString();
                    m.CreateTime = DateTime.Now;
                    m.Remark = "";
                    // 
                    if (item.LCLMxCreateInput.Count > 0)
                    { 
                        JIT_RemainderLCL.InsertAsync(m);
                        foreach (var tmp in item.LCLMxCreateInput)
                        {
                            Entities.RemainderLCLMx Mx = new Entities.RemainderLCLMx()
                            {
                                LCLMxId = Guid.NewGuid().ToString(),
                                RemainderLCLId = m.LCLId,
                                ICMOInspectBillId = tmp.ICMOInspectBillId,
                                SpelledQty = tmp.SpelledQty,
                                LCLMxTime = DateTime.Now
                            };
                            JIT_RemainderLCLMx.InsertAsync(Mx);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                EX(-1, "创建失败", "请稍后再试！" + e.Message);
            }
        }
    }

}

