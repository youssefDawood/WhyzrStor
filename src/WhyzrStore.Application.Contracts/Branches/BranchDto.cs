using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace WhyzrStore.Branches
{
    public class BranchDto  : AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string EditorName { get; set; }
        public string CreatorName { get; set; }
    }
}
