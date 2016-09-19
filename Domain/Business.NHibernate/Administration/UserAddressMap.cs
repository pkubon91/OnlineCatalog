using System;
using Business.Administration;
using FluentNHibernate.Mapping;

namespace Business.NHibernate.Administration
{
    public class UserAddressMap : ClassMap<UserAddress>
    {
        public UserAddressMap()
        {
            Table("USER_ADDRESS");

            Id(a => a.UniqueId).GeneratedBy.Foreign("USER_GUID").Column("USER_ADDRESS_GUID");
            Map(a => a.BuildingNumber).Column("BUILDING_NUMBER");
            Map(a => a.City).Column("CITY");
            Map(a => a.Email).Column("EMAIL");
            Map(a => a.PhoneNumber).Column("PHONE_NUMBER");
            Map(a => a.Street).Column("STREET");
        }
    }
}
