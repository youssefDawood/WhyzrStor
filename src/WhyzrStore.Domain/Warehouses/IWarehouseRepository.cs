using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WhyzrStore.Warehouses
{
    public interface IWarehouseRepository : IRepository<Warehouse, Guid>, IQueryable
    {
        

        Task<List<Warehouse>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
