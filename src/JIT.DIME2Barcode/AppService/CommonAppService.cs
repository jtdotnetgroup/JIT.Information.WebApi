using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JIT.DIME2Barcode.Model;
using CommonTools;
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
            [Description(",ZLJYYJY,")] 质量检验已检验
        }

        /// <summary>
        /// 查询任务数量
        /// </summary>
        public List<TaskQty> GetTaskQty(string StrKey)
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
                    Total = JIT_VW_MODispBillList.GetAll()
                        .Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 0).Count(),
                    BZ = "待开工任务数量"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验.ToDescription().Replace(",", ""),
                    Total = JIT_VW_MODispBillList.GetAll().Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 1).Count(),
                    BZ = "质量检验待检验数量"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.库存查询.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.库存查询.ToDescription().Replace(",", ""),
                    Total = JIT_VM_Inventory.GetAll().Count(),
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
                    Total = 0,
                    BZ = ""
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.派工任务待汇报.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.派工任务待汇报.ToDescription().Replace(",", ""),
                    Total = JIT_VW_MODispBillList.GetAll().Where(w => w.操作者 == AbpSession.UserId && w.FStatus == 1).Count(),
                    BZ = "派工任务待汇报"
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验待检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验待检验.ToDescription().Replace(",", ""),
                    Total = 0,
                    BZ = ""
                };
                listTaskQty.Add(tmpTaskQty);
            }

            if (isAll == "*" || StrKey.Contains(TaskType.质量检验已检验.ToDescription()))
            {
                TaskQty tmpTaskQty = new TaskQty()
                {
                    StrKey = TaskType.质量检验已检验.ToDescription().Replace(",", ""),
                    Total = 0,
                    BZ = ""
                };
                listTaskQty.Add(tmpTaskQty);
            }
            // 所有枚举信息
            string[] ListTaskType = new string[Enum.GetValues(typeof(TaskType)).Length];
            int i = 0;
            foreach (TaskType TY in Enum.GetValues(typeof(TaskType)))
            {
                ListTaskType[i] = TY.ToDescription();
                i++;
            }

            if (isAll != "*" && !ListTaskType.Contains(StrKey))
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
    }
}
