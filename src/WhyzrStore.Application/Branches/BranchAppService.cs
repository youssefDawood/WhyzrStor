using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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
using WhyzrStore.Users;

namespace WhyzrStore.Branches
{
    public class BranchAppService : CrudAppService<Branch, BranchDto, Guid,
        PagedAndSortedResultRequestDto, CreateBranchDto, UpdateBranchDto>,
        IBranchAppService
    {
        private readonly IRepository<AppUser, Guid> _userRepository;
        public BranchAppService(
          IRepository<AppUser, Guid> userRepository
            , IRepository<Branch, Guid> repository)
            : base(repository)
        {
            _userRepository = userRepository;
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
            if (BranchDto.CreatorId.HasValue)
            {
                BranchDto.CreatorName = _userRepository
                    .FirstOrDefault(user => user.Id == BranchDto.CreatorId).Name;
            }
            if (BranchDto.LastModifierId.HasValue)
            {
                BranchDto.EditorName = _userRepository
                   .FirstOrDefault(user => user.Id == BranchDto.CreatorId).Name;
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

            var BranchDtos = queryResult.Select(x =>
           {
               var BranchDto = ObjectMapper.Map<Branch, BranchDto>(x.branch);
               if (BranchDto.ParentId.HasValue)
               {

                   var parent = Repository.FirstOrDefault(b => b.Id == BranchDto.ParentId);
                   if (parent != null)
                   {
                       BranchDto.ParentName = parent.Name;
                   }
               }
               if (BranchDto.CreatorId.HasValue)
               {
                   BranchDto.CreatorName = _userRepository
                       .FirstOrDefault(user => user.Id == BranchDto.CreatorId).Name;
               }
               if (BranchDto.LastModifierId.HasValue)
               {
                   BranchDto.EditorName = _userRepository
                      .FirstOrDefault(user => user.Id == BranchDto.CreatorId).Name;
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

        public async Task<PagedResultDto<BranchDto>> GetSupBranchesToMainBranch(Guid branchId)
        {
            await CheckGetListPolicyAsync();
            var branches = Repository.Where(b => b.ParentId.HasValue&& b.Id == branchId || b.ParentId == branchId).Select(b => b.Id).ToList();
            bool check = true;
            while (check)
            {
                var newBranches = Repository.Where(b => branches.Any(id => id == b.ParentId.Value) && !branches.Any(id => id == b.Id))
                    .Select(b => b.Id).ToList();
                if (newBranches.Count == 0)
                {
                    check = false;
                }
                else
                {
                    branches.AddRange(newBranches);
                }
            }

            var listBranches = Repository.Where(b => b.ParentId.HasValue).Where(b => branches.Any(id => id == b.Id)).ToList();
            var listchildren = listBranches.Where(b => !listBranches.Any(l => b.Id == l.ParentId.Value)).ToList();
            var totalCount = listchildren.Count();
            var warehousesDto = ObjectMapper.Map<List<Branch>, List<BranchDto>>(listchildren);
            return new PagedResultDto<BranchDto>(
                totalCount,
                warehousesDto
            );
        }

        public override Task<BranchDto> CreateAsync(CreateBranchDto input)
        {

            //    using var image = Image.Load(input.ImageUrl.OpenReadStream());
            //var path = "C:\\project\\WhyzrStore\\src\\WhyzrStore.Domain\\NewFolder";
            //image.Mutate(x => x.Resize(100, 100));
            //image.Save(path); 
            //image.Mutate(x => x.Resize(256, 256));
            //image.Save(path);   
            //image.Mutate(x => x.Resize(600, 600));
            //image.Save(path);

            return base.CreateAsync(input);
        }
    }
}

