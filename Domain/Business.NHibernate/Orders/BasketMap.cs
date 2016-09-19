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
            References(basket => basket.Owner).Not.Nullable().Column("USER_GUID");
            References(basket => basket.BasketShop).Not.Nullable().Column("SHOP_GUID");

            HasManyToMany(basket => basket.BasketProducts)
                .Cascade.AllDeleteOrphan()
                .ChildKeyColumn("PRODUCT_GUID")
                .ParentKeyColumn("BASKET_GUID")
                .Table("BASKET_PRODUCTS");
        }
    }
}
