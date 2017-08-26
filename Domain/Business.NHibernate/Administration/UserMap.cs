using FluentNHibernate.Mapping;
using Business.Administration;

namespace Business.NHibernate.Administration
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("APP_USER");

            Id(u => u.UniqueId).GeneratedBy.GuidComb().Column("APP_USER_GUID");
            Map(u => u.Name).Not.Nullable().Column("NAME");
            Map(u => u.Surname).Not.Nullable().Column("SURNAME");
            Map(u => u.UserRank).Not.Nullable().Column("USER_RANK");
            Map(u => u.Login).Not.Nullable().Column("LOGIN");
            Map(u => u.Password).Not.Nullable().Column("PASSWORD");

            References(u => u.Address).Not.Nullable().Cascade.All().Unique().Column("USER_ADDRESS_GUID").LazyLoad(Laziness.False);
            HasManyToMany(user => user.AssignedShops)
                .Cascade.Merge()
                .ChildKeyColumn("SHOP_GUID")
                .ParentKeyColumn("APP_USER_GUID")
                .LazyLoad().FetchType.Select()
                .Table("USER_SHOPS");
        }
    }
}
