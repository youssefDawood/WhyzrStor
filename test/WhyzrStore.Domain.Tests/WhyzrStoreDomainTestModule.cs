using WhyzrStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace WhyzrStore
{
    [DependsOn(
        typeof(WhyzrStoreEntityFrameworkCoreTestModule)
        )]
    public class WhyzrStoreDomainTestModule : AbpModule
    {

    }
}