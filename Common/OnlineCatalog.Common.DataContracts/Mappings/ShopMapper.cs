using System;
using AutoMapper;
using Business.Groups;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Common.DataContracts.Mappings
{
    public static class ShopMapper
    {
        public static Shop Map(this ShopDto shop)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<ShopDto, Shop>()
                        .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address.Map()))
                        .ForMember(dest => dest.UniqueId, opts => opts.MapFrom(src => new Guid(src.ShopGuid.ToString()))));
            return Mapper.Map<Shop>(shop);
        }

        public static ShopDto Map(this Shop shop)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<Shop, ShopDto>()
                        .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address.Map()))
                        .ForMember(dest => dest.ShopGuid, opts => opts.MapFrom(src => new Guid(src.UniqueId.ToString()))));
            return Mapper.Map<ShopDto>(shop);
        }
    }
}
