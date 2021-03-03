using WhyzrStore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace WhyzrStore.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(WhyzrStoreEntityFrameworkCoreDbMigrationsModule),
        typeof(WhyzrStoreApplicationContractsModule)
        )]
    public class WhyzrStoreDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
