using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using JIT.DIME2Barcode.Entities;
using JIT.InformationSystem;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.BackgroudJobs
{
    public class Sync_SubMessages:PeriodicBackgroundWorkerBase,ISingletonDependency
    {
        private IRepository<t_SubMessage> SMRepository { get; set; }
        private IRepository<t_SubMessage_Sync> SM_SyncRepository { get; set; }
        private IRepository<SyncRecord> SRepository { get; set; }

        public Sync_SubMessages(AbpTimer timer,IRepository<t_SubMessage> repository,IRepository<t_SubMessage_Sync> repository_sync,IRepository<SyncRecord> sRepository) : base(timer)
        {
            SMRepository = repository;
            SM_SyncRepository = repository_sync;
            SRepository = sRepository;
            timer.Period = 1000*60*10;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            var sync_record = SRepository.GetAll().SingleOrDefaultAsync(p => p.TableName == "t_SubMessage");

            if (sync_record == null)
            {

            }
        }
    }
}