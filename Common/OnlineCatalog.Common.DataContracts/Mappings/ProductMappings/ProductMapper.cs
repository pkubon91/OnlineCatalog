using System;
using System.Linq;
using AutoMapper;
using Business.Groups;
using Business.Products;
using DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Common.DataContracts.Mappings.ProductMappings
{
    public static class ProductMapper
    {
        public static Product Map(this ProductDto productDto)
        {
            if(productDto == null) throw new ArgumentNullException(nameof(productDto));
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductDto, Product>()
                        .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.ProductCategories.Select(x => new ProductCategory() {UniqueId = x})))
                        .ForMember(dest => dest.UniqueId, opts => opts.MapFrom(src => src.ProductGuid))
                        .ForMember(dest => dest.AssignedShop, opts => opts.MapFrom(src => new Shop {UniqueId = src.AssignedShop}))
                        .ForMember(dest => dest.CreatedBy, opts => opts.MapFrom(src => src.CreatedBy.Map())));
            return Mapper.Map<Product>(productDto);
        }

        public static ProductDto Map(this Product product)
        {
            if(product == null) throw new ArgumentNullException(nameof(product));
            Mapper.Initialize(
                m =>
                    m.CreateMap<Product, ProductDto>()
                        .ForMember(dest => dest.ProductGuid, opts => opts.MapFrom(src => src.UniqueId))
                        .ForMember(dest => dest.AssignedShop, opts => opts.MapFrom(src => src.AssignedShop.UniqueId))
                        .ForMember(dest => dest.CreatedBy, opts => opts.Ignore()));
            return Mapper.Map<ProductDto>(product);
        }
    }
}
