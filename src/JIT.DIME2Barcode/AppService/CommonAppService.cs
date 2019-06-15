using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using JIT.DIME2Barcode.Model;
using CommonTools;
using JIT.InformationSystem.Authorization.Users;
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
            [Description(",BZYS,")] 包装余数
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
                    Total = await JIT_VW_MODispBillList.GetAll().Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 1).CountAsync(),
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
                    Total = await JIT_ICMODispBill.GetAll().Join(JIT_ICMOInspectBill.GetAll(), A => A.FID, B => B.ICMODispBillID,
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
                        }).Where(A => A.FStatus >= PublicEnum.ICMODispBillState.已检验.EnumToInt() && A.FYSQty > 0).CountAsync(),
                    BZ = "包装余数"
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

        public async void Test(int RoleId)
        {
            var r = ABP_UserRole.GetAll().ToList();
            // 全部
            var result = await ABP_User.GetAll()
                .GroupJoin(ABP_UserRole.GetAll(), A => A.Id, B => B.UserId,
                    (A, B) => new UserRole
                    {
                        UserId = A.Id,
                        RoleId = B.FirstOrDefault().RoleId
                    })
                .Where(w => w.RoleId == RoleId || w.RoleId == null)
                .ToListAsync();
            // 是角色
            var result1 = await ABP_User.GetAll()
                .Join(ABP_UserRole.GetAll(), A => A.Id, B => B.UserId,
                    (A, B) => new UserRole {UserId = A.Id, RoleId = B.RoleId})
                .Where(w => w.RoleId == RoleId)
                .ToListAsync();
            // 不是角色
            var result2 = await ABP_User.GetAll()
                .Where(w => !ABP_UserRole.GetAll().Where(q => q.RoleId == RoleId).Select(s => s.UserId)
                    .Contains(w.Id))
                .Select(s => new UserRole {UserId = s.Id, RoleId = (int?) null})
                .ToListAsync();

            var list = result.MapTo<List<UserRole>>();
            var list1 = result1.MapTo<List<UserRole>>();
            var list2 = result2.MapTo<List<UserRole>>();
        }
    }

}
