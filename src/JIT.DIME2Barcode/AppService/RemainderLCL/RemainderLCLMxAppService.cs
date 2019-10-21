 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using Microsoft.EntityFrameworkCore;

 namespace JIT.DIME2Barcode.AppService.RemainderLCL
{
    /// <summary>
    /// 余数拼箱明细表
    /// </summary>
    public class RemainderLCLMxAppService: BaseAppService
    {
        /// <summary>
        /// 余数拼箱明细
        /// </summary>
        public async Task<List<Entities.RemainderLCLMx>> GetAll(string RemainderLCLId)
        {
            return await JIT_RemainderLCLMx.GetAll().Where(w=>w.RemainderLCLId.Equals(RemainderLCLId)).ToListAsync();
        }
    }
}
