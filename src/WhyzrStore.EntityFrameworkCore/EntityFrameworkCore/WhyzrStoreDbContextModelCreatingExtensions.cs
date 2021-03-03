using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace WhyzrStore.EntityFrameworkCore
{
    public static class WhyzrStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureWhyzrStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(WhyzrStoreConsts.DbTablePrefix + "YourEntities", WhyzrStoreConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}