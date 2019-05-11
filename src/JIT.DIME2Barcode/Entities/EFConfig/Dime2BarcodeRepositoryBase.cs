using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace JIT.DIME2Barcode.Entities.EFConfig
{
    public class Dime2BarcodeRepositoryBase<TEntity,TPrimaryKey>:EfCoreRepositoryBase<Dime2barcodeContext,TEntity,TPrimaryKey> where TEntity:class,IEntity<TPrimaryKey>
    {
        public Dime2BarcodeRepositoryBase(IDbContextProvider<Dime2barcodeContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class Dime2BarcodeRepositoryBase<TEntity> : EfCoreRepositoryBase<Dime2barcodeContext, TEntity,int> where TEntity : class, IEntity<int>
    {
        public Dime2BarcodeRepositoryBase(IDbContextProvider<Dime2barcodeContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}