using System;
using Abp.Domain.Entities;

namespace JIT.InformationSystem
{
    public class SyncRecord : Entity
    {
        //同步的表名
        public string TableName { get; set; }

        //最后同步时间
        public DateTime LastSyncTime { get; set; }

        public int MaxRowVersion { get; set; }
    }

}
