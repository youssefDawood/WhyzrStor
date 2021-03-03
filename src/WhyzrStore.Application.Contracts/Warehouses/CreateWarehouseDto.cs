using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhyzrStore.Warehouses
{
   public class CreateWarehouseDto
    {
        public Guid BranchId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
