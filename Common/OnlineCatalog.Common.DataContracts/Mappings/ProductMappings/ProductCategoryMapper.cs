using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business.Products;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Common.DataContracts.Mappings.ProductMappings
{
    public static class ProductCategoryMapper
    {
        public static ProductCategory Map(this ProductCategoryDto productCategoryDto)
        {
            if(productCategoryDto == null) throw new ArgumentNullException(nameof(productCategoryDto));
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductCategoryDto, ProductCategory>()
                        .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.CreatedDateTime))
                        .ForMember(dest => dest.Updated, opts => opts.MapFrom(src => src.UpdatedDateTime))
                        .ForMember(dest => dest.UniqueId, opts => opts.MapFrom(src => new Guid(src.ProductCategoryGuid.ToString())))
                        .ForMember(dest => dest.AssignedProducts, opts => opts.Ignore()));
            return Mapper.Map<ProductCategory>(productCategoryDto);
        }

        public static ProductCategoryDto Map(this ProductCategory productCategory)
        {
            if(productCategory == null) throw new ArgumentNullException(nameof(productCategory));
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductCategory, ProductCategoryDto>()
                        .ForMember(dest => dest.CreatedDateTime, opts => opts.MapFrom(src => src.Created))
                        .ForMember(dest => dest.UpdatedDateTime, opts => opts.MapFrom(src => src.Updated))
                        .ForMember(dest => dest.ProductCategoryGuid, opts => opts.MapFrom(src => new Guid(src.UniqueId.ToString())))
                        .ForMember(dest => dest.ShopGuid, opts => opts.MapFrom(src => new Guid(src.ProductCategoryShop.UniqueId.ToString())))
                        .ForMember(dest => dest.AssignedProducts, opts => opts.Ignore()));
            return Mapper.Map<ProductCategoryDto>(productCategory);
        }
    }
}
