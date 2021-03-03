using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WhyzrStore.Branches;
using WhyzrStore.Warehouses;

namespace WhyzrStore.EntityFrameworkCore
{
    public static class WhyzrStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureWhyzrStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Branch>(b=>
            {
                b.ToTable(WhyzrStoreConsts.DbTablePrefix + "Branches");
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasMaxLength(128);
                b.HasIndex(x => x.Name);
                b.HasOne<Branch>().WithMany().HasForeignKey(x => x.ParentId);
            });

            builder.Entity<Warehouse>(b =>
            {
                b.ToTable(WhyzrStoreConsts.DbTablePrefix + "Warehouses");
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasMaxLength(128);
                b.HasIndex(x => x.Name);
                b.HasOne<Branch>().WithMany().HasForeignKey(x => x.BranchId).IsRequired();
            });
           
        }
    }
}