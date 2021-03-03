using AutoMapper;
using WhyzrStore.Branches;
using WhyzrStore.Warehouses;

namespace WhyzrStore
{
    public class WhyzrStoreApplicationAutoMapperProfile : Profile
    {
        public WhyzrStoreApplicationAutoMapperProfile()
        {
            CreateMap<Branch, BranchDto>();
            CreateMap< CreateBranchDto, Branch>();
            CreateMap< UpdateBranchDto, Branch>();
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<CreateWarehouseDto, Warehouse>();
            CreateMap<UpdateWarehouseDto, Warehouse>();
          
        }
    }
}
