using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools
{
    public class PublicEnum
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            男=1,
            女=2
        }

        /// <summary>
        /// 员工表状态职位
        /// </summary>
        public enum FWorkingState 
        {
            在职=1,
            离职=2
        }

        /// <summary>
        /// 组织类型
        /// </summary>
        public enum OrganizationType
        {
            集团=1,
            公司=2,
            部门=3,
        }

        public enum EquipmentType
        {
            设备 = 1, 模具 = 2, 其它 = 999
        }

        public enum TimeUnit
        {
            小时=1,分钟=0,天=2
        }
        /// <summary>
        /// 任务单号状态
        /// </summary>
        public enum ICMODispBillState
        {
            待开工= 0,
            待汇报 = 1,
            已检验 = 2,
        }

        public enum CompareOperate
        {
            DateTime,Number,String,Boolean
        }



        

    }
}
