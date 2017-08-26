using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business.Administration;
using Business.Groups;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Mappings;

namespace DataContracts.Mappings
{
    public static class UserMapper
    {
        public static User Map(this UserDto dto)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<UserDto, User>()
                        .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address.Map()))
                        .ForMember(dest => dest.UserRank, opts => opts.MapFrom(src => src.UserRank))
                        .ForMember(dest => dest.AssignedShops, opts => opts.MapFrom(src => GetShops(src))));

            return Mapper.Map<User>(dto);
        }

        private static IEnumerable<Shop> GetShops(UserDto src)
        {
            if (src.AssignedShops == null) return Enumerable.Empty<Shop>();
            return src.AssignedShops.Select(s => s.Map());
        }

        public static UserDto Map(this User domainUser)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<User, UserDto>()
                        .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address.Map()))
                        .ForMember(dest => dest.UserRank, opts => opts.MapFrom(src => src.UserRank))
                        .ForMember(dest => dest.AssignedShops, opts => opts.Ignore())
                        .ForMember(dest => dest.UserGuid, opts => opts.MapFrom(src => src.UniqueId)));
            return Mapper.Map<UserDto>(domainUser);
        }

        private static IEnumerable<ShopDto> GetShops(User src)
        {
            if (src.AssignedShops == null) return Enumerable.Empty<ShopDto>();
            return src.AssignedShops.Select(s => s.Map());
        }
    }
}
