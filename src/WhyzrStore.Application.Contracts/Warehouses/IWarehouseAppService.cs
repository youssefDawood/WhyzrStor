using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WhyzrStore.Warehouses
{
    public interface IWarehouseAppService : ICrudAppService<
          WarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
           CreateWarehouseDto, UpdateWarehouseDto>
    {
        
        public  Task<PagedResultDto<WarehouseDto>> GetListWarehousesToBranchAsync(Guid branshId);
    }
}