using System.Linq;
using AutoMapper;
using Business.Orders;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Orders;

namespace OnlineCatalog.Common.DataContracts.Mappings
{
    public static class BasketMapper
    {
        public static BasketDto Map(this Basket basket)
        {
            Mapper.Initialize(m => m.CreateMap<Basket, BasketDto>()
                .ForMember(x => x.BasketGuid, opts => opts.MapFrom(src => src.UniqueId))
                .ForMember(x => x.Products, opts => opts.MapFrom(src => src.BasketProducts.Select(p => p.Map())))
                .ForMember(x => x.OrderState, opts => opts.MapFrom(src => (int)src.State))
                .ForMember(x => x.ShopGuid, opts => opts.MapFrom(src => src.BasketShop.UniqueId))
                .ForMember(x => x.UserAssigned, opts => opts.MapFrom(src => src.Owner.Login))
                .ForMember(x => x.UpdateDate, opts => opts.MapFrom(src => src.Updated)));

            return Mapper.Map<BasketDto>(basket);
        }
    }
}
