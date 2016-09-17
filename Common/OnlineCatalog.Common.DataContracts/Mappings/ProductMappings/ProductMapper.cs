﻿using System;
using System.Linq;
using AutoMapper;
using Business.Products;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Common.DataContracts.Mappings.ProductMappings
{
    public static class ProductMapper
    {
        public static Product Map(this ProductDto productDto)
        {
            if(productDto == null) throw new ArgumentNullException(nameof(productDto));
            Mapper.Initialize(m => m.CreateMap<ProductDto, Product>().ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.ProductCategories.Select(x => x.Map()))));
            return Mapper.Map<Product>(productDto);
        }

        public static ProductDto Map(this Product product)
        {
            if(product == null) throw new ArgumentNullException(nameof(product));
            Mapper.Initialize(m => m.CreateMap<Product, ProductDto>().ForMember(dest => dest.ProductCategories, opts => opts.MapFrom(src => src.Categories.Select(x => x.Map()))));
            return Mapper.Map<ProductDto>(product);
        }
    }
}
