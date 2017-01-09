using AutoMapper;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Web.Main.Models.Shared;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class AddressViewMapper
    {
        public static UserAddressDto Map(this AddressViewModel address)
        {
            Mapper.Initialize(m => m.CreateMap<AddressViewModel, UserAddressDto>());
            return Mapper.Map<UserAddressDto>(address);
        }

        public static AddressViewModel Map(this UserAddressDto address)
        {
            Mapper.Initialize(m => m.CreateMap<UserAddressDto, AddressViewModel>());
            return Mapper.Map<AddressViewModel>(address);
        }
    }
}