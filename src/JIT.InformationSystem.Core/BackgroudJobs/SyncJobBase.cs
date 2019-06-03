using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace JIT.InformationSystem
{
    /// <summary>
    /// 同步作业基类
    /// </summary>
    /// <typeparam name="TSRepository">源数据仓储</typeparam>
    /// <typeparam name="TDRepository">目标数据仓储</typeparam>
    /// <typeparam name="TArgs">同步参数</typeparam>
    public abstract class SyncJobBase<TSRepository, TDRepository, TArgs> : BackgroundJob<TArgs>,ISyncJob,ISingletonDependency
        where TSRepository : IRepository
        where TDRepository : IRepository
    {
        protected TSRepository tSRepository { get; set; }
        protected TDRepository tDRepository { get; set; }
        protected IRepository<SyncRecord> syncRepository { get; set; }

        //同步表名称
        protected string JobName { get; set; }

        public SyncJobBase(TSRepository tSRepository, TDRepository tDRepository, IRepository<SyncRecord> syncRepository)
        {
            this.tSRepository = tSRepository;
            this.tDRepository = tDRepository;
            this.syncRepository = syncRepository;
        }
    }
}