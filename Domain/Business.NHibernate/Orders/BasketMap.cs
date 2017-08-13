using Business.Orders;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Orders
{
    public class BasketMap : ClassMap<Basket>
    {
        public BasketMap()
        {
            Table("BASKET");

            Id(basket => basket.UniqueId).GeneratedBy.GuidComb().Column("BASKET_GUID");
            Map(basket => basket.Created).Not.Nullable().Column("CREATE_DATE");
            Map(basket => basket.Updated).Not.Nullable().Column("UPDATE_DATE");
            Map(basket => basket.IsRealized).Not.Nullable().Default("0").Column("IS_REALIZED");
            Map(basket => basket.State).Not.Nullable().Default("0").Column("STATE");
            References(basket => basket.Owner).Cascade.Merge().LazyLoad(Laziness.False).Not.Nullable().Column("USER_GUID");
            References(basket => basket.BasketShop).Cascade.Merge().LazyLoad(Laziness.False).Not.Nullable().Column("SHOP_GUID");

            HasManyToMany(basket => basket.BasketProducts)
                .Not.LazyLoad()
                .Cascade.Merge()
                .ParentKeyColumn("BASKET_GUID")
                .ChildKeyColumn("PRODUCT_GUID")
                .Table("BASKET_PRODUCTS");
        }
    }
}
