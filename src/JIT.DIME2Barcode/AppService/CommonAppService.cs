using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using JIT.DIME2Barcode.Model;
using CommonTools;
using JIT.DIME2Barcode.TaskAssignment.Test.Dtos;
using JIT.InformationSystem.Authorization.Users;
using JIT.InformationSystem.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 通用的
    /// </summary>
    public class CommonAppService : BaseAppService
    {
        #region 触屏端任务

        /// <summary>
        /// 任务类型 key
        /// </summary>
        public enum TaskType
        {
            [Description(",PGRW,")] 派工任务,
            [Description(",ZLJY,")] 质量检验,
            [Description(",TMDY,")] 条码打印,
            [Description(",KCCX,")] 库存查询,
            [Description(",PGRWDKG,")] 派工任务待开工,
            [Description(",PGRWDHB,")] 派工任务待汇报,
            [Description(",ZLJYDJY,")] 质量检验待检验,
            [Description(",ZLJYYJY,")] 质量检验已检验,
            [Description(",BZYS,")] 包装余数,
            [Description(",PXJL,")] 拼箱记录
        }

        /// <summary>
        /// 查询任务数量
        /// </summary>
        public async Task<List<TaskQty>> GetTaskQty(string StrKey)
        {
            string isAll = StrKey;
            StrKey = "," + StrKey + ",";
            List<TaskQty> listTaskQty = new List<TaskQty>();
            // 开始形成数据
            if (isAll == "*" || StrKey.Contains(TaskType.派工任务.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.派工任务.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll()
                        .Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 0).CountAsync(),
                    BZ = "待开工任务数量"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll().Where(w => w.FStatus == 1).CountAsync(),
                    BZ = "质量检验待检验数量"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.库存查询.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.库存查询.ToDescription().Replace(",", ""),
                    Total = await JIT_VM_Inventory.GetAll().CountAsync(),
                    BZ = "库存查询数量"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.条码打印.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.条码打印.ToDescription().Replace(",", ""),
                    Total = 0,
                    BZ = ""
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.派工任务待开工.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.派工任务待开工.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll()
                        .Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 0).CountAsync(),
                    BZ = "派工任务待开工"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.派工任务待汇报.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.派工任务待汇报.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll()
                        .Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 1).CountAsync(),
                    BZ = "派工任务待汇报"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验待检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验待检验.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll().Where(w => w.FStatus == 1).CountAsync(),
                    BZ = "质量检验待检验"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验已检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验已检验.ToDescription().Replace(",", ""),
                    Total = await JIT_VW_MODispBillList.GetAll().Where(w => w.FStatus == 2).CountAsync(),
                    BZ = "质量检验已检验"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.包装余数.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.包装余数.ToDescription().Replace(",", ""),
                    Total = await JIT_ICMODispBill.GetAll().Join(JIT_ICMOInspectBill.GetAll(), A => A.FID,
                            B => B.ICMODispBillID,
                            (A, B) => new
                            {
                                A.FBillNo,
                                A.FBiller,
                                A.FDate,
                                FBillNo2 = B.FBillNo,
                                B.BatchNum,
                                B.FYSQty,
                                B.FInspector,
                                B.FInspectTime,
                                A.employee.FName,
                                //
                                A.FStatus
                            }).Where(A => A.FStatus >= PublicEnum.ICMODispBillState.已检验.EnumToInt() && A.FYSQty > 0)
                        .CountAsync(),
                    BZ = "包装余数"
                };
                listTaskQty.Add(tmpTaskQty);
            }
            if (isAll == "*" || StrKey.Contains(TaskType.拼箱记录.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.拼箱记录.ToDescription().Replace(",", ""),
                    Total = await JIT_RemainderLCL.GetAll().CountAsync(),
                    BZ = "拼箱记录"
                };
                listTaskQty.Add(tmpTaskQty);
            }
            // 所有枚举信息
            string[] listTaskType = new string[Enum.GetValues(typeof(TaskType)).Length];
            int i = 0;
            foreach (TaskType ty in Enum.GetValues(typeof(TaskType)))
            {
                listTaskType[i] = ty.ToDescription();
                i++;
            }

            if (isAll != "*" && !listTaskType.Contains(StrKey))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = StrKey.Replace(",", ""),
                    Total = 0,
                    BZ = StrKey.Replace(",", "") + ",未定义,所以数量为0"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            return listTaskQty;
        }

        #endregion

        /// <summary>
        /// 查询所属角色所有用户
        /// </summary>
        /// <param name="input">查询条件</param>
        public async void GetUserRole(UserRoleInput input)
        {
            // 全部
            var result = from a in ABP_User.GetAll()
                join r in ABP_UserRole.GetAll().Where(w => w.RoleId == input.RoleId) on a.Id equals r.UserId into g1
                from g in g1.DefaultIfEmpty()
                select new UserRole {UserId = a.Id, RoleId = g.RoleId, UserName = a.UserName};
            // 全部
            if (input.type == 1)
            {

            }
            // 是角色
            else if (input.type == 2)
            {
                result = result.Where(w => w.RoleId == input.RoleId);
            }
            // 不是角色
            else if (input.type == 3)
            {
                result = result.Where(w => w.RoleId == null);
            }

            // 开始查询
            result = result.Where(w => w.UserName.Contains(input.UserName));
            //
            var count = result.Count();
            var data = await result.OrderBy(o => o.RoleId).PageBy(input)
                .ToListAsync();
            var resultList = new PagedResultDto<UserRole>(count, data); 
        }
    }

    
}
