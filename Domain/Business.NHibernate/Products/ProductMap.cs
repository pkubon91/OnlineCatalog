using Business.Products;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Products
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("PRODUCT");

            Id(prod => prod.UniqueId).GeneratedBy.GuidComb().Column("PRODUCT_GUID");
            Map(prod => prod.Created).Not.Nullable().Column("ADDED_DATE");
            Map(prod => prod.Updated).Not.Nullable().Column("UPDATED_DATE");
            Map(prod => prod.ProductName).Not.Nullable().Length(50).Column("PRODUCT_NAME");
            Map(prod => prod.Description).Length(1000).Column("DESCRIPTION");
            Map(prod => prod.IsDeleted).Not.Nullable().Default("0").Column("IS_DELETED");
            Map(prod => prod.IsActive).Not.Nullable().Default("1").Column("IS_ACTIVE");
            Map(prod => prod.DefaultPrice).Not.Nullable().Default("0").Column("DEFAULT_PRICE");
            Map(prod => prod.Tax).Not.Nullable().Column("TAX").Default("0");
            Map(prod => prod.ProductImage).Nullable().Column("PRODUCT_IMAGE");

            References(prod => prod.CreatedBy).Not.Nullable().Cascade.All().LazyLoad(Laziness.False).Column("USER_GUID");
            References(prod => prod.AssignedShop).Not.Nullable().LazyLoad(Laziness.False).Column("SHOP_GUID");

            HasManyToMany(prod => prod.Categories)
                .Cascade.Merge()
                .Table("PRODUCT_ASSIGNMENT")
                .ParentKeyColumn("PRODUCT_GUID")
                .ChildKeyColumn("PRODUCT_CATEGORY_GUID")
                .LazyLoad().Fetch.Select();

            HasManyToMany(prod => prod.BasketProducts)
                .ChildKeyColumn("BASKET_GUID")
                .ParentKeyColumn("PRODUCT_GUID")
                .Cascade.AllDeleteOrphan()
                .Table("BASKET_PRODUCTS")
                .LazyLoad().Fetch.Select();
        }
    }
}
