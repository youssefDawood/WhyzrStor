using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WhyzrStore.Branches
{
    public interface IBranchRepository : IRepository<Branch, Guid>, IQueryable
    {


        Task<List<Branch>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
