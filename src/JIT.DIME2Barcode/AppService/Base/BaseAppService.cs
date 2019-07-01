﻿using Abp.Application.Services;
using Abp.Authorization.Users;
﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.UI;
using JIT.DIME2Barcode.Entities;
 using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
 using JIT.InformationSystem.Authorization.Users;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseAppService: ApplicationService 
    {
        #region 所有表、视图、存储过程

        #region ABP 系统表
        /// <summary>
        /// 用户表
        /// </summary>
        public IRepository<User, long> ABP_User { get; set; }
        /// <summary>
        /// 角色表
        /// </summary>
        public IRepository<UserRole, long> ABP_UserRole { get; set; }
        /// <summary>
        /// 日志记录表
        /// </summary>
        public IRepository<Abpauditlogs, long> LogsRepository { get; set; }
        #endregion

        #region 业务表
        // 表 
        public IRepository<ICMODaily, string> JIT_ICMODaily { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRepository<DIME2Barcode.Entities.ICMOSchedule, string> JIT_ICMOSchedule { get; set; }
        /// <summary>
        /// 派工异常记录
        /// </summary>
        public IRepository<DIME2Barcode.Entities.ICException, string> JIT_ICException { get; set; }
        /// <summary>
        /// 任务派工单
        /// </summary>
        public IRepository<DIME2Barcode.Entities.ICMODispBill, string> JIT_ICMODispBill { get; set; }
        /// <summary>
        /// 任务派工单记录表
        /// </summary>
        public IRepository<DIME2Barcode.Entities.ICMODispBillRecord, string> JIT_ICMODispBillRecord { get; set; }
        /// <summary>
        /// 质量检验单
        /// </summary>
        public IRepository<ICMOInspectBill, string> JIT_ICMOInspectBill { get; set; }
        /// <summary>
        /// 员工表
        /// </summary>
        public IRepository<Employee, int> JIT_Employee { get; set; }
        /// <summary>
        /// 质量检验记录
        /// </summary>
        public IRepository<DIME2Barcode.Entities.ICQualityRpt, string> JIT_ICQualityRpt { get; set; }
        /// <summary>
        /// 工序不良项目表
        /// </summary>
        public IRepository<DIME2Barcode.Entities.TB_BadItemRelation, int> JIT_TB_BadItemRelation { get; set; }
        /// <summary>
        /// 余数拼箱
        /// </summary>
        public IRepository<DIME2Barcode.Entities.RemainderLCL, string> JIT_RemainderLCL { get; set; }
        /// <summary>
        /// 余数拼箱明细表
        /// </summary>
        public IRepository<DIME2Barcode.Entities.RemainderLCLMx, string> JIT_RemainderLCLMx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRepository<DIME2Barcode.Entities.t_ICItem, int> JIT_t_ICItem { get; set; } 
        /// <summary>
        /// 组织架构
        /// </summary>
        public IRepository<t_OrganizationUnit, int> JIT_t_OrganizationUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRepository<DIME2Barcode.Entities.Equipment, int> JIT_t_equipment { get; set; }
        #endregion

        #region 系统表
        public IRepository<DIME2Barcode.Entities.Sys_Task, int> JIT_Sys_Task { get; set; }
        public IRepository<DIME2Barcode.Entities.Sys_TaskRecord, int> JIT_Sys_TaskRecord { get; set; }
        public IRepository<DIME2Barcode.Entities.Sys_BasicInfo, int> JIT_Sys_BasicInfo { get; set; }
        #endregion

        #region 视图
        /// <summary>
        /// 
        /// </summary>
        public IRepository<DIME2Barcode.Entities.VW_MODispBillList, string> JIT_VW_MODispBillList { get; set; }
        /// <summary>
        /// 库存查询
        /// </summary>
        public IRepository<DIME2Barcode.Entities.VM_Inventory, int> JIT_VM_Inventory { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public IRepository<VW_ICMOInspectBillList, string> JIT_VW_ICMOInspectBillList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRepository<VM_ICMOInspectBillED, string> JIT_VM_ICMOInspectBillED { get; set; }
        /// <summary>
        /// 日计划任务明细
        /// </summary>
        public IRepository<VW_DispatchBill_List, string> JIT_VW_DispatchBill_List { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRepository<VW_ICMODispBill_By_Date, string> JIT_VW_ICMODispBill_By_Date { get; set; }
        #endregion

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
