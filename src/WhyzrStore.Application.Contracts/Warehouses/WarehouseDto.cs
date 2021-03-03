using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace WhyzrStore.Warehouses
{
   public class WarehouseDto : AuditedEntityDto<Guid>
    {
        public Guid BranchId { get; set; }
        public string  Name { get; set; }
        public string BranchName { get; set; }
    }
}
