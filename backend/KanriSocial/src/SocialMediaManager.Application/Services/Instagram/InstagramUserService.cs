using FluentResults;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Shared.Dtos.Instagram;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Services.Instagram;

public class InstagramUserService(IInstagramClient instagramClient, IInstagramUserRepository instagramUserRepository) : IInstagramUserService
{
    private readonly IInstagramClient _instagramClient = instagramClient;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;

    public async Task<Result<InstagramUserDetail>> GetInstagramUserDetailById(Guid userId)
    {
        var instagramUser = await _instagramUserRepository.GetByUserId(userId);
        if (instagramUser is null)
        {
            return Result.Fail("Instagram user not found.");
        }
        
        return await _instagramClient.GetUserDetail(instagramUser.AccountId, instagramUser.Token);
    }
}