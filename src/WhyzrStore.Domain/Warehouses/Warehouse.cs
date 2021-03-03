using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using WhyzrStore.Branches;

namespace WhyzrStore.Warehouses
{
   public class Warehouse : AuditedAggregateRoot<Guid> , IMultiTenant
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        public virtual Guid? TenantId { get; set; }
    }
}
