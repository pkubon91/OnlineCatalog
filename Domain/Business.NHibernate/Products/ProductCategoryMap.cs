using Business.Products;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Products
{
    public class ProductCategoryMap : ClassMap<ProductCategory>
    {
        public ProductCategoryMap()
        {
            Table("PRODUCT_CATEGORY");

            Id(cat => cat.UniqueId).GeneratedBy.GuidComb().Column("PRODUCT_CATEGORY_GUID");
            Map(cat => cat.CategoryName).Not.Nullable().Length(100).Column("PRODUCT_CATEGORY_NAME");

            HasManyToMany(cat => cat.AssignedProducts)
                .Cascade.All()
                .Inverse()
                .Table("PRODUCT_ASSIGNMENT")
                .ParentKeyColumn("PRODUCT_CATEGORY_GUID")
                .ChildKeyColumn("PRODUCT_GUID");
        }
    }
}
