using System;
using System.Collections.Generic;
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
        /// 员工表职位
        /// </summary>
        public enum FSystemUser
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
            车间=4
        }


    }
}
