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
           
        }
    }
}