using AutoMapper;
using TheJituEcommerce_Auth.Models;
using TheJituEcommerce_Auth.Models.DTOs;

namespace TheJituEcommerce_Auth.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            //settting the email as the username
            CreateMap<RegisterRequestDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
