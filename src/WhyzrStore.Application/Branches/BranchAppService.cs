using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using WhyzrStore.Permissions;

namespace WhyzrStore.Branches
{
    public class BranchAppService : CrudAppService<Branch, BranchDto, Guid,
        PagedAndSortedResultRequestDto, CreateBranchDto, UpdateBranchDto>,
        IBranchAppService
    {
        public BranchAppService(IRepository<Branch, Guid> repository)
            : base(repository)
        {
            GetPolicyName = WhyzrStorePermissions.Branches.Defult;
            CreatePolicyName = WhyzrStorePermissions.Branches.Create;
            UpdatePolicyName = WhyzrStorePermissions.Branches.Edit;
            DeletePolicyName = WhyzrStorePermissions.Branches.Delete;
            GetListPolicyName = WhyzrStorePermissions.Branches.Defult;
        }

        public override async Task<BranchDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();
            var query = from branch in Repository
                        where branch.Id == id
                        select new { branch };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Branch), id);
            }

            var BranchDto = ObjectMapper.Map<Branch, BranchDto>(queryResult.branch);
            if (BranchDto.ParentId.HasValue)
            {
                var queryParent = from parent in Repository
                                  where parent.Id == BranchDto.ParentId
                                  select new { parent };
                var queryParentResult = await AsyncExecuter.FirstOrDefaultAsync(queryParent);
                if (queryParentResult != null)
                {
                    BranchDto.ParentName = queryParentResult.parent.Name;
                }
            }
            return BranchDto;
        }

        public override async Task<PagedResultDto<BranchDto>> GetListAsync(
           PagedAndSortedResultRequestDto input)
        {
            await CheckGetListPolicyAsync();

          
 
            var query = from branch in Repository
                      orderby input.Sorting
                      select new { branch };
            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);
           
            var BranchDtos = queryResult.Select( x =>
            {
                var BranchDto = ObjectMapper.Map<Branch, BranchDto>(x.branch);
                if (BranchDto.ParentId.HasValue)
                {
                    
                    var parent =    Repository.FirstOrDefault(b=>b.Id== BranchDto.ParentId);
                    if (parent != null)
                    {
                        BranchDto.ParentName =parent.Name;
                    }
                }
                return BranchDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BranchDto>(
                totalCount,
                BranchDtos
            );
        }

    }
}

