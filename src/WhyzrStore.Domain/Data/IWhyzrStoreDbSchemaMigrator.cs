using System.Threading.Tasks;

namespace WhyzrStore.Data
{
    public interface IWhyzrStoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
