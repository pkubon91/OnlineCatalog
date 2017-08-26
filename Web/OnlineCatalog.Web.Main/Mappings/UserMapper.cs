using AutoMapper;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.Mappings
{
    public static class UserMapper
    {
        public static UserViewModel Map(this UserDto user)
        {
            Mapper.Initialize(x => x.CreateMap<UserDto, UserViewModel>().ForMember(u => u.UserRank, opts => opts.MapFrom(src => src.UserRank.ToString())));
            return Mapper.Map<UserViewModel>(user);
        }
    }
}