using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.UI;
using CommonTools;
using JIT.DIME2Barcode.Model;
using JIT.DIME2Barcode.TaskAssignment.Test.Dtos;
using JIT.InformationSystem.CommonClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

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

        #region 测试
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
                select new UserRole { UserId = a.Id, RoleId = g.RoleId, UserName = a.UserName };
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

            input.UserName = input.UserName ?? "";
            // 开始查询
            result = result.Where(w => w.UserName.StartsWith(input.UserName));
            //
            var count = result.Count();
            var data = await result.OrderBy(o => o.RoleId).PageBy(input)
                .ToListAsync();
            var resultList = new PagedResultDto<UserRole>(count, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName">方法名称，包含程序集名、类型名、方法名称格式如：assembly.class.method</param>
        /// <returns></returns>
        public List<JITQueryFormDto> GetQueryFields(string methodFullName)
        {
            var arr = methodFullName.Split("#");
            if (arr.Length != 3)
            {
                throw new UserFriendlyException("传入方法名称有误");
            }

            string assemblyName = arr[0];
            string className = arr[1];
            string methodName = arr[2];

            //通过程序集名、类名获取类型
            var t = ReflectionHelper.GetClassType(className, assemblyName);

            MethodInfo method = null;

            try
            {
                method = t.GetMethod(methodName);

                var result = GetParamsInof(method);

                return result;
            }
            catch (AmbiguousMatchException e)
            {
                Console.WriteLine(e.Message);
                throw new UserFriendlyException($"【{className}】中存在多个【{methodName}】名称的方法");
            }

        }

        /// <summary>
        /// 获取方法参数信息
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private List<JITQueryFormDto> GetParamsInof(MethodInfo method)
        {
            Dictionary<string, string> fielDictionary = new Dictionary<string, string>();
            fielDictionary.Add("system.int64", "int");
            fielDictionary.Add("system.string", "string");
            fielDictionary.Add("system.boolean", "bool");
            fielDictionary.Add("system.int32", "int");
            fielDictionary.Add("system.double", "double");
            fielDictionary.Add("system.datetime", "datetime");
            fielDictionary.Add("system.decimal", "double");

            //不返回字段
            var pisfilter = new[] { "SkipCount", "MaxResultCount", "Where", "Sorting" };
            

            var paramTypes = method.GetParameters();
            if (paramTypes.Length != 1)
            {
                throw new UserFriendlyException($"【{method.Name}】方法参数多于1个");
            }

            var parType = paramTypes[0].ParameterType;
            var pis = parType.GetProperties().Where(p=>!pisfilter.Contains(p.Name)).ToList();

            List<JITQueryFormDto> result = new List<JITQueryFormDto>();
            JITQueryFormDto item = null;

            pis.ForEach(p =>
            {
                //获取字段DisplayName特性
                var disp = p.GetCustomAttributes<DisplayNameAttribute>().ToList();
                //获取字段显示名称
                //var display = disp.Count == 1 ? disp[0].DisplayName : p.Name;
                string  display = disp.Select(s => s.DisplayName).FirstOrDefault() ?? p.Name;
                
                //获取字段类型名称
                var protyneName = p.PropertyType.ToString().Replace("System.Nullable`1","").Replace("[","").Replace("]","").ToLower();

                item = new JITQueryFormDto();
                item.DispName = display;
                item.Name = p.Name;
                
                if (p.PropertyType.IsEnum)
                {
                    item.FieldType = p.PropertyType.IsEnum ? "select" : fielDictionary[protyneName];
                    var values= Enum.GetValues(p.PropertyType);
                    item.Values=new List<object>();

                    foreach (var v in values)
                    {
                        var val = Enum.Parse(p.PropertyType, v.ToString());
                        item.Values.Add(new{value=(int )val,title=v.ToString()});
                    }
                }
                else
                {
                    item.FieldType = fielDictionary.Keys.Contains(protyneName)
                        ? fielDictionary[protyneName]
                        : p.PropertyType.Name;
                }
                // 
                if ( disp.Where(w=>w.DisplayName.Length>0).Count()==0) { }
                else
                {
                    result.Add(item);
                }
            });
            return result;

        }
        #endregion
    }


}
