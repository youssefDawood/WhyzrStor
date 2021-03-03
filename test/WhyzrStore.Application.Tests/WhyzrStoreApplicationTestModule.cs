using Volo.Abp.Modularity;

namespace WhyzrStore
{
    [DependsOn(
        typeof(WhyzrStoreApplicationModule),
        typeof(WhyzrStoreDomainTestModule)
        )]
    public class WhyzrStoreApplicationTestModule : AbpModule
    {

    }
}