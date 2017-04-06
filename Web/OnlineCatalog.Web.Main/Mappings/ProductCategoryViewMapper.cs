using AutoMapper;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Models.Products;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class ProductCategoryViewMapper
    {
        public static ProductCategoryViewModel Map(this ProductCategoryDto productCategory)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductCategoryDto, ProductCategoryViewModel>()
                        .ForMember(s => s.ProductCategoryName, opts => opts.MapFrom(src => src.CategoryName))
                        .ForMember(s => s.ProductCategoryGuid, opts => opts.MapFrom(src => src.ProductCategoryGuid)));
            return Mapper.Map<ProductCategoryViewModel>(productCategory);
        }

        public static ProductCategoryDto Map(this ProductCategoryViewModel productCategory)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<ProductCategoryViewModel, ProductCategoryDto>()
                        .ForMember(s => s.CategoryName, opts => opts.MapFrom(src => src.ProductCategoryName))
                        .ForMember(s => s.ProductCategoryGuid, opts => opts.MapFrom(src => src.ProductCategoryGuid)));
            return Mapper.Map<ProductCategoryDto>(productCategory);
        }
    }
}