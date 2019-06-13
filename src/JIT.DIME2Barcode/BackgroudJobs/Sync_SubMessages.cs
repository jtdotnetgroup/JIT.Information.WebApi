using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.BackgroudJobs
{
    public class Sync_SubMessages:PeriodicBackgroundWorkerBase,ISingletonDependency
    {
        private IRepository<t_SubMessage> SRepository { get; set; }

        public Sync_SubMessages(AbpTimer timer,IRepository<t_SubMessage> repository) : base(timer)
        {
            SRepository = repository;
            timer.Period = 1000*60*10;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            
        }
    }
}