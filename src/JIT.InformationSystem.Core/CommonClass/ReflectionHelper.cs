using System;
using System.Linq;
using System.Reflection;
using Abp.Application.Services;
using Abp.UI;

namespace JIT.InformationSystem.CommonClass
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 通过程序集名、类名反射获得类形Type
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="assemblyName">程序集名</param>
        /// <returns></returns>
        public static Type GetClassType(string className,string assemblyName)
        {
            Assembly ass = null;
            try
            {
                ass = Assembly.Load(assemblyName);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
            
            // 过滤条件
            var classList = ass.GetTypes()
                .Where(p => p.IsPublic && p.IsSubclassOf(typeof(ApplicationService)) && p.Name == className).ToList();

            if (classList.Count == 0)
            {
                throw new UserFriendlyException($"找不到【{className}】类");
            }
            else if (classList.Count > 1)
            {
                throw new UserFriendlyException($"程序集【{assemblyName}】中存在多个【{className}】类");
            }

            var t = classList.SingleOrDefault();

            return t;
        }

        
    }
}