using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace WhyzrStore.EntityFrameworkCore
{
    [DependsOn(
        typeof(WhyzrStoreEntityFrameworkCoreModule)
        )]
    public class WhyzrStoreEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<WhyzrStoreMigrationsDbContext>();
        }
    }
}
