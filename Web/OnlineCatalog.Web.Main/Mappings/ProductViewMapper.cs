using System;
using System.Linq;
using AutoMapper;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Models.Products;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class ProductViewMapper
    {
        public static ProductViewModel Map(this ProductDto productDto)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductDto, ProductViewModel>()
                        .ForMember(s => s.TaxPercent, opts => opts.MapFrom(src => src.Tax))
                        .ForMember(s => s.ShopGuid, opts => opts.MapFrom(src => src.AssignedShop)));
            return Mapper.Map<ProductViewModel>(productDto);
        }

        public static ProductDto Map(this ProductViewModel viewModel)
        {
            Mapper.Initialize(m => m.CreateMap<ProductViewModel, ProductDto>()
                .ForMember(s => s.AssignedShop, opts => opts.MapFrom(src => src.ShopGuid))
                .ForMember(s => s.CreatedBy, opts => opts.MapFrom(src => new UserDto {Login = src.CreatedLogin}))
                .ForMember(s => s.ProductCategories, opts => opts.MapFrom(src => src.AssignedCategories.Select(Guid.Parse)))
                .ForMember(s => s.Tax, opts => opts.MapFrom(src => src.TaxPercent)));
            return Mapper.Map<ProductDto>(viewModel);
        }
    }
}