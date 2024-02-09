using AutoMapper;
using Stock.Core.Entites.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.API.DTOS;
using Stock.Core.Entites;

namespace Stock.Core.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, ItemToReturnDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<ItemType, ItemTypeDTo>().ReverseMap();
            CreateMap<ItemToReturnDto, ItemDto>().ReverseMap();
            CreateMap<Item, ItemDto>()
                .ForMember(i=>i.ItemTypeName,o=>o.MapFrom(t=>t.ItemType.Name))
                .ForMember(i => i.SupplierName, o => o.MapFrom(t => t.Supplier.Name))
                .ReverseMap();
        }
    }
}
