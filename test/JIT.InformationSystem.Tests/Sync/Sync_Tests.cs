using System;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Reflection;
using JIT.DIME2Barcode.BackgroudJobs;
using Xunit;

namespace JIT.InformationSystem.Tests.Sync
{
    public class Sync_Tests:InformationSystemTestBase
    {
        public SyncItemJob ItemJob { get; set; }
        private ITypeFinder typeFinder { get; set; }

        public Sync_Tests(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        [Fact]
        public async Task Sync_Item()
        {
            var jobs= typeFinder.Find(t =>
            {
                var type = t.GetTypeInfo();
                //反射所有公共的、非抽象类、且实现了ISyncJob的类
                return type.IsPublic 
                       && !type.IsAbstract 
                       && type.IsClass 
                       && typeof(ISyncJob).IsAssignableFrom(type);
            });

            Console.WriteLine(jobs.Length);
        }
    }
}