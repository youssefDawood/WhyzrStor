using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace WhyzrStore.Warehouses
{
    public class GetWarehouseListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
