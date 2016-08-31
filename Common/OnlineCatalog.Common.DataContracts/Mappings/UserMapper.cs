using AutoMapper;
using Business.Administration;
using OnlineCatalog.Common.DataContracts.Administration;
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
                        .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address.Map())));

            return Mapper.Map<User>(dto);
        }

        public static UserDto Map(this User domainUser)
        {
            Mapper.Initialize(
                m =>
                    m.CreateMap<User, UserDto>().ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Map())));
            return Mapper.Map<UserDto>(domainUser);
        }
    }
}
