using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Castle.Core.Logging;
using JIT.DIME2Barcode.Entities;
using JIT.InformationSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace JIT.DIME2Barcode.BackgroudJobs
{
    [DisplayName("物料同步")]
    public class SyncItemJob: SyncJobBase<IRepository<t_ICItem>,IRepository<ICItem_Sync>,DateTime>,ITransientDependency
    {
        public ILogger logger { get; set; }

        public SyncItemJob(IRepository<t_ICItem> tSRepository, IRepository<ICItem_Sync> tDRepository, IRepository<SyncRecord> syncRepository)
            : base(tSRepository, tDRepository, syncRepository)
        {
            this.JobName = "t_ICItem";
        }

        [UnitOfWork]
        public  override async void Execute(DateTime args)
        {
            try
            {
                //查询最后同步的记录
                var query = await syncRepository.GetAll().ToListAsync();
                var lastSync = query.SingleOrDefault(p => p.TableName == this.JobName);
                //最后更新行版本，此值是由金蝶数据库维护，每次插入或更新时都会更新此值，所以取最大值就是最新的记录
                var maxRowVersion = await tSRepository.GetAll().MaxAsync(p => p.FRowVersion);

                //如果没有同步记录则进行全表分页同步
                if (lastSync == null)
                {
                    lastSync = new SyncRecord()
                    {
                        TableName = JobName,
                        LastSyncTime = DateTime.Now,
                        MaxRowVersion = maxRowVersion
                    };
                    //记录同步时间
                    await SyncByPaging();
                    await syncRepository.InsertAsync(lastSync);
                }
                else
                {
                    //修改最后同步时间
                    lastSync.LastSyncTime = DateTime.Now;
                    lastSync.MaxRowVersion = maxRowVersion;
                    await syncRepository.UpdateAsync(lastSync);
                    await AppendSync(lastSync);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        //分页同步
        private async Task<int> SyncByPaging()
        {
            var page = new PagedResultRequestDto() { SkipCount = 0,MaxResultCount = 500};
            var count = await tSRepository.CountAsync();
            var syncCount = 0;
            //计算总页数
            var totalPage = count % 500 > 0 ? count / page.MaxResultCount + 1 : count / page.MaxResultCount;
            var dcontext = tDRepository.GetDbContext() as ProductionPlanMySqlDbContext;

            if (dcontext==null)
            {
                throw new AbpException("未找到数据库连接上下文");
            }

            for (int i = 0; i <= totalPage; i++)
            {
                page.SkipCount = i;
                //分页数据
                var data = await tSRepository.GetAll().OrderBy("FItemID").PageBy(page).ToListAsync();

                if (data.Count > 0)
                {
                    await dcontext.AddRangeAsync(data.MapTo<List<ICItem_Sync>>());

                    syncCount += data.Count;
                }
            }

            await dcontext.SaveChangesAsync();

            return syncCount;
        }

        //追加同步
        private async Task<int> AppendSync(SyncRecord record)
        {
            //查出自上次同步后更新的物料记录
            var data=await tSRepository.GetAll().Where(p => p.FRowVersion >= record.MaxRowVersion).ToListAsync();
            //最后更新行版本，此值是由金蝶数据库维护，每次插入或更新时都会更新此值，所以取最大值就是最新的记录
            var maxRowVersion =await tSRepository.GetAll().MaxAsync(p => p.FRowVersion);

            if (data.Count>0)
            {
                foreach (var item in data)
                {
                    var entity = item.MapTo<ICItem_Sync>();
                    await tDRepository.InsertOrUpdateAsync(entity);
                }
            }

            record.MaxRowVersion = maxRowVersion;
            await syncRepository.UpdateAsync(record);

            return data.Count;

        }

    }
}