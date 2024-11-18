using FluentResults;
using SocialMediaManager.Application.Services.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;
using TikTokUser = SocialMediaManager.Domain.Models.TikTok.TikTokUser;

namespace SocialMediaManager.Application.Services.TikTok;

public class TikTokPhotoService(
    ITikTokClient tikTokClient, 
    ITokenEncryptor tokenEncryptor,
    ITikTokPhotoRepository tikTokPhotoRepository) : ITikTokPhotoService
{
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokPhotoRepository _tikTokPhotoRepository = tikTokPhotoRepository;
    
    public async Task<Result<Guid>> SchedulePhoto(TikTokContent content, TikTokUser tikTokUser, Guid photoId)
    {
        var encryptedToken = _tokenEncryptor.Decrypt(tikTokUser.Token);
        var refreshTokenResult = await _tikTokClient.GetToken(encryptedToken, TikTokTokenType.RefreshToken);
        if (refreshTokenResult.IsFailed || refreshTokenResult.Value == null)
        {
            return Result.Fail("Failed to get refresh token.");
        }

        var result = await _tikTokClient.PostContent(content, refreshTokenResult.Value.AccessToken);
        if (result.IsFailed || result.Value == null)
        {
            return Result.Fail(result.Errors);
        }
        
        var tikTokPhoto = await _tikTokPhotoRepository.GetById(photoId);
        if (tikTokPhoto == null)
        {
            return Result.Fail("Photo not found.");
        }
        
        tikTokPhoto.IsPublished = true;
        tikTokPhoto.PublishId = result.Value.Data.PublishId;
        
        await _tikTokPhotoRepository.Update(tikTokPhoto);
        
        return Result.Ok(photoId);
    }
}