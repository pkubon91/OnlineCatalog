using AutoMapper;
using Business.Administration;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Common.DataContracts.Mappings
{
    public static class UserAddressMapper
    {
        public static UserAddress Map(this UserAddressDto dto)
        {
            if (dto == null) return null;
            Mapper.Initialize(m => m.CreateMap<UserAddressDto, UserAddress>());
            return Mapper.Map<UserAddress>(dto);
        }

        public static UserAddressDto Map(this UserAddress domainObject)
        {
            if (domainObject == null) return null;
            Mapper.Initialize(m => m.CreateMap<UserAddress, UserAddressDto>());
            return Mapper.Map<UserAddressDto>(domainObject);
        }
    }
}
