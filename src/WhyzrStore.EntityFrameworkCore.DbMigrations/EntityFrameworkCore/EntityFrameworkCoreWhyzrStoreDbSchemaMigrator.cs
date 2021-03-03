using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhyzrStore.Data;
using Volo.Abp.DependencyInjection;

namespace WhyzrStore.EntityFrameworkCore
{
    public class EntityFrameworkCoreWhyzrStoreDbSchemaMigrator
        : IWhyzrStoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreWhyzrStoreDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the WhyzrStoreMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WhyzrStoreMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}