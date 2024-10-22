using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Services.Instagram.Interfaces;

public interface IInstagramUserService
{
    Task<Result<InstagramUserDetail>> GetInstagramUserDetailById(Guid userId);
}