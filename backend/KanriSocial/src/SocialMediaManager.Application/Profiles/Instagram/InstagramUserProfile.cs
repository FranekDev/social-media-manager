using AutoMapper;
using SocialMediaManager.Domain.Models.Instagram.User;
using SocialMediaManager.Application.Dtos.Instagram;

namespace SocialMediaManager.Application.Profiles.Instagram;

public class InstagramUserProfile : Profile
{
    public InstagramUserProfile()
    {
        CreateMap<InstagramUserDto, InstagramUser>();
        CreateMap<InstagramUser, InstagramUserDto>();
    }
}