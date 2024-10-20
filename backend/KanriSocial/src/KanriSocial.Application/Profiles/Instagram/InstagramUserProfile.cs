using AutoMapper;
using KanriSocial.Application.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.User;

namespace KanriSocial.Application.Profiles.Instagram;

public class InstagramUserProfile : Profile
{
    public InstagramUserProfile()
    {
        CreateMap<InstagramUserDto, InstagramUser>();
        CreateMap<InstagramUser, InstagramUserDto>();
    }
}