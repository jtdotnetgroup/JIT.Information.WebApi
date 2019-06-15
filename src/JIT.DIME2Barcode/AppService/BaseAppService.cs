using Abp.Application.Services;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.UI;
using JIT.DIME2Barcode.Entities;
using JIT.InformationSystem.Authorization.Users;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseAppService: ApplicationService 
    {
        #region 所有表、视图、存储过程
        // ABP 系统表
        public IRepository<User, long> ABP_User { get; set; }
        public IRepository<UserRole, long> ABP_UserRole { get; set; }
        // 表
        public IRepository<DIME2Barcode.Entities.ICException, string> JIT_ICException { get; set; }
        public IRepository<DIME2Barcode.Entities.ICMODispBill, string> JIT_ICMODispBill { get; set; }
        public IRepository<DIME2Barcode.Entities.ICMODispBillRecord, string> JIT_ICMODispBillRecord { get; set; }
        public IRepository<ICMOInspectBill, string> JIT_ICMOInspectBill { get; set; }
        public IRepository<Employee, int> JIT_Employee { get; set; }
        // 视图
        public IRepository<DIME2Barcode.Entities.VW_MODispBillList, string> JIT_VW_MODispBillList { get; set; }
        public IRepository<DIME2Barcode.Entities.VM_Inventory, int> JIT_VM_Inventory { get; set; }
        #endregion

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="message">信息</param>
        /// <param name="details">明细</param>
        public void EX(int code = -1, string message = "请求无效", string details = "")
        {
            throw new UserFriendlyException(code, message, details);
        }
    }
}
