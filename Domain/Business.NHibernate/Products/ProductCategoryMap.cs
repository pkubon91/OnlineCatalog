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
            Map(cat => cat.Created).Not.Nullable().Column("CREATED_DATE");
            Map(cat => cat.Updated).Not.Nullable().Column("UPDATED_DATE");

            References(cat => cat.ProductCategoryShop).Not.Nullable().Cascade.All().Column("SHOP_GUID");
            HasManyToMany(cat => cat.AssignedProducts)
                .Cascade.All()
                .Inverse()
                .Table("PRODUCT_ASSIGNMENT")
                .ParentKeyColumn("PRODUCT_CATEGORY_GUID")
                .ChildKeyColumn("PRODUCT_GUID");
        }
    }
}
