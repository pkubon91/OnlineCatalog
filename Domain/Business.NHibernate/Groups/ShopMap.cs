using Business.Groups;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Groups
{
    public class ShopMap : ClassMap<Shop>
    {
        public ShopMap()
        {
            Table("SHOP");

            Id(shop => shop.UniqueId).GeneratedBy.GuidComb().Column("SHOP_GUID");
            Map(shop => shop.Name).Not.Nullable().Column("NAME");
            Map(shop => shop.Created).Not.Nullable().Column("CREATE_DATE");
            Map(shop => shop.Updated).Not.Nullable().Column("UPDATE_DATE");
            Map(shop => shop.IsActive).Not.Nullable().Column("IS_ACTIVE");
            Map(shop => shop.IsDeleted).Not.Nullable().Column("IS_DELETED");

            References(shop => shop.Address).Not.Nullable().Cascade.All().Unique().Column("USER_ADDRESS_GUID");
            HasManyToMany(shop => shop.AssignedUsers)
                .Cascade.AllDeleteOrphan()
                .ChildKeyColumn("APP_USER_GUID")
                .ParentKeyColumn("SHOP_GUID")
                .Table("USER_SHOPS");
        }
    }
}
