using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Infrastructure.Clients.Interfaces;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;
using KanriSocial.Shared.Dtos.Instagram;

namespace KanriSocial.Application.Services.Instagram;

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