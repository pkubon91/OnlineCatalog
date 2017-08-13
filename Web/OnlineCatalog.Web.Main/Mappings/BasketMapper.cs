using System.Linq;
using AutoMapper;
using OnlineCatalog.Common.DataContracts.Orders;
using OnlineCatalog.Web.Main.Models.Basket;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class BasketMapper
    {
        public static BasketViewModel Map(this BasketDto basket)
        {
            Mapper.Initialize(m => m.CreateMap<BasketDto, BasketViewModel>()
                .ForMember(b => b.Products, opts => opts.MapFrom(src => src.Products.Select(p => p.Map())))
                .ForMember(b => b.OrderState, opts => opts.MapFrom(src => src.OrderState))
                .ForMember(b => b.UpdateDateTime, opts => opts.MapFrom(src => src.UpdateDate)));

            return Mapper.Map<BasketViewModel>(basket);
        }
    }
}