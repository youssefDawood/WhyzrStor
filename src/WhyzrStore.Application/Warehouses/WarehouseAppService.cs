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

namespace WhyzrStore.Warehouses
{

    public class WarehouseAppService : CrudAppService<Warehouse, WarehouseDto, Guid,
        PagedAndSortedResultRequestDto, CreateWarehouseDto, UpdateWarehouseDto>, IWarehouseAppService
    {
        private readonly IRepository<Branch, Guid> _branchRepository;

        public WarehouseAppService(
            IRepository<Branch, Guid> branchRepository, 
             IRepository<Warehouse, Guid> repository)
            : base(repository)
        {
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
                return warehouseDto;
            }).ToList();

            //Get the total count  
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<WarehouseDto>(
                totalCount,
                warehouseDtos
            );
        }

       

    }
}  