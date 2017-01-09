using AutoMapper;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Models.Groups;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class ShopViewMapper
    {
        public static ShopDto Map(this ShopViewModel shopViewModel)
        {
            Mapper.Initialize(m => m.CreateMap<ShopViewModel, ShopDto>()
                .ForMember(s => s.Name, opts => opts.MapFrom(src => src.ShopName))
                .ForMember(s => s.Address, opts => opts.MapFrom(src => src.Address.Map())));
            return Mapper.Map<ShopDto>(shopViewModel);
        }

        public static ShopViewModel Map(this ShopDto shopDto)
        {
            Mapper.Initialize(m => m.CreateMap<ShopDto, ShopViewModel>()
                .ForMember(s => s.ShopName, opts => opts.MapFrom(src => src.Name))
                .ForMember(s => s.Address, opts => opts.MapFrom(src => src.Address.Map())));
            return Mapper.Map<ShopViewModel>(shopDto);
        }
    }
}