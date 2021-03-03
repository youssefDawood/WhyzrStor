using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace WhyzrStore.Branches
{
   public class Branch : AuditedAggregateRoot<Guid>, IMultiTenant
    {
         
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public virtual Guid? TenantId { get; set; }
    }
}
