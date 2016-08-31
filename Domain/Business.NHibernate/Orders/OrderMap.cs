using Business.Orders;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Orders
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("SHOP_ORDER");

            Id(order => order.UniqueId).GeneratedBy.GuidComb().Column("SHOP_ORDER_GUID");
            Map(order => order.FinalPrice).Not.Nullable().Column("FINAL_PRICE");
            Map(order => order.Created).Not.Nullable().Column("CREATE_DATE");
            Map(order => order.Updated).Not.Nullable().Column("UPDATE_DATE");

            References(order => order.Seller).Not.Nullable().Column("USER_GUID");
            References(order => order.BasketToOrder).Nullable().Unique().Column("BASKET_GUID");
        }
    }
}
