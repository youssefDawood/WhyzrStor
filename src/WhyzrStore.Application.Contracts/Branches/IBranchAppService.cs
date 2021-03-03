using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WhyzrStore.Branches
{
    public interface IBranchAppService : ICrudAppService<
           BranchDto,
            Guid,
            PagedAndSortedResultRequestDto,
           CreateBranchDto, UpdateBranchDto>
    {

    }
}