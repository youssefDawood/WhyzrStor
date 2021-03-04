using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using WhyzrStore.Branches;
using WhyzrStore.Permissions;
using WhyzrStore.Users;

namespace WhyzrStore.Warehouses
{

    public class WarehouseAppService : CrudAppService<Warehouse, WarehouseDto, Guid,
        PagedAndSortedResultRequestDto, CreateWarehouseDto, UpdateWarehouseDto>, IWarehouseAppService
    {
        private readonly IRepository<Branch, Guid> _branchRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        public WarehouseAppService(
              IRepository<AppUser, Guid> userRepository,
            IRepository<Branch, Guid> branchRepository, 
             IRepository<Warehouse, Guid> repository)
            : base(repository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            GetPolicyName = WhyzrStorePermissions.Warehouses.Defult;
            CreatePolicyName = WhyzrStorePermissions.Warehouses.Create;
            UpdatePolicyName = WhyzrStorePermissions.Warehouses.Edit;
            DeletePolicyName = WhyzrStorePermissions.Warehouses.Delete;
            GetListPolicyName = WhyzrStorePermissions.Warehouses.Defult;
        }

        public override async Task<WarehouseDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            //Prepare a query to join books and authors
            var query = from warehouse in Repository
                        join branch in _branchRepository on warehouse.BranchId equals branch.Id
                        where warehouse.Id == id
                        select new { warehouse, branch };

            //Execute the query and get the book with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Warehouse), id);
            }

            var warehouseDto = ObjectMapper.Map<Warehouse, WarehouseDto>(queryResult.warehouse);
            warehouseDto.BranchName = queryResult.branch.Name;
            if (warehouseDto.CreatorId.HasValue)
            {
                warehouseDto.CreatorName = _userRepository
                    .FirstOrDefault(user => user.Id == warehouseDto.CreatorId).Name;
            }
            if (warehouseDto.LastModifierId.HasValue)
            {
                warehouseDto.EditorName = _userRepository
                   .FirstOrDefault(user => user.Id == warehouseDto.CreatorId).Name;
            }
            return warehouseDto;
        }

        public override async Task<PagedResultDto<WarehouseDto>> GetListAsync(
           PagedAndSortedResultRequestDto input)
        {
            await CheckGetListPolicyAsync();

        
            var query = from warehouse in Repository
                        join branch in _branchRepository on warehouse.BranchId equals branch.Id 
                        orderby input.Sorting
                        select new { warehouse, branch };

            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of warehouseDto objects
            var warehouseDtos = queryResult.Select(x =>
            {
                var warehouseDto = ObjectMapper.Map<Warehouse, WarehouseDto>(x.warehouse);
                warehouseDto.BranchName = x.branch.Name;
                if (warehouseDto.CreatorId.HasValue)
                {
                    warehouseDto.CreatorName = _userRepository
                        .FirstOrDefault(user => user.Id == warehouseDto.CreatorId).Name;
                }
                if (warehouseDto.LastModifierId.HasValue)
                {
                    warehouseDto.EditorName = _userRepository
                       .FirstOrDefault(user => user.Id == warehouseDto.CreatorId).Name;
                }
                return warehouseDto;
            }).ToList();

            //Get the total count  
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<WarehouseDto>(
                totalCount,
                warehouseDtos
            );
        }


        public  async Task<PagedResultDto<WarehouseDto>> GetListWarehousesToBranchAsync(Guid branchId)

        {
            await CheckGetListPolicyAsync();
      
            var branches = _branchRepository.Where(b => b.Id == branchId || b.ParentId == branchId).Select(b=> b.Id).ToList();
       
            bool check = false;
                
                while (check) 
                {
                var newBranches = _branchRepository.Where(b => branches.Any(id => id == b.Id) 
                || b.ParentId != null ? branches.Any(id => id == b.ParentId) : false)
                    .Select(b => b.Id).ToList();
                if (branches == newBranches) 
                {
                    check = true;
                } 
                else
                {
                    branches = newBranches;
                }
            }
            
            var lisWarehouses = Repository.Where(w=> branches.Any(id=>id ==w.BranchId)).ToList();
            var totalCount = lisWarehouses.Count();
            var warehousesDto = ObjectMapper.Map<List<Warehouse>, List<WarehouseDto> >(lisWarehouses);
            return new PagedResultDto<WarehouseDto>(
                totalCount,
                warehousesDto
            );
        }

    }
}  