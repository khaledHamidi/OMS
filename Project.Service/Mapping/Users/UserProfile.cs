using AutoMapper;
using OMS.Core.DTOs.Users;
using OMS.Core.Entities;

namespace OMS.Service.Mapping.Users;

public class UserProfile: Profile
{
    public UserProfile() 
    {
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<User, UserLoggedDto>();
    }
}
