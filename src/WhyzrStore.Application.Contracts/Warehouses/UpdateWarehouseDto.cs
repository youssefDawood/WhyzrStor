using System;
using System.Collections.Generic;
using System.Text;

namespace WhyzrStore.Warehouses
{
    public class UpdateWarehouseDto :CreateWarehouseDto
    {
        public Guid Id { get; set; }
    }
}
