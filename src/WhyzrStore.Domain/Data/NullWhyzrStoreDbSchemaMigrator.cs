using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WhyzrStore.Data
{
    /* This is used if database provider does't define
     * IWhyzrStoreDbSchemaMigrator implementation.
     */
    public class NullWhyzrStoreDbSchemaMigrator : IWhyzrStoreDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}