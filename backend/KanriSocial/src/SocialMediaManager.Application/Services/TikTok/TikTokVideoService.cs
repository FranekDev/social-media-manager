using FluentResults;
using SocialMediaManager.Application.Services.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;
using TikTokUser = SocialMediaManager.Domain.Models.TikTok.TikTokUser;

namespace SocialMediaManager.Application.Services.TikTok;

public class TikTokVideoService(
    ITikTokClient tikTokClient,
    ITokenEncryptor tokenEncryptor,
    ITikTokVideoRepository tikTokVideoRepository) : ITikTokVideoService
{
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokVideoRepository _tikTokVideoRepository = tikTokVideoRepository;
    
    public async Task<Result<Guid>> ScheduleVideo(TikTokPostInfo postInfo, TikTokSurceInfo sourceInfo, TikTokUser tiktokUser, Guid videoId)
    {
        var encryptedToken = _tokenEncryptor.Decrypt(tiktokUser.Token);
        var refreshTokenResult = await _tikTokClient.GetToken(encryptedToken, TikTokTokenType.RefreshToken);
        if (refreshTokenResult.IsFailed || refreshTokenResult.Value == null)
        {
            return Result.Fail("Failed to get refresh token.");
        }
        
        var result = await _tikTokClient.PostVideo(postInfo, sourceInfo, refreshTokenResult.Value.AccessToken);
        if (result.IsFailed || result.Value == null)
        {
            return Result.Fail(result.Errors);
        }
        
        var tikTokVideo = await _tikTokVideoRepository.GetById(videoId);
        if (tikTokVideo == null)
        {
            return Result.Fail("Video not found.");
        }
        
        tikTokVideo.IsPublished = true;
        tikTokVideo.PublishId = result.Value.Data.PublishId;
        await _tikTokVideoRepository.Update(tikTokVideo);
        
        return Result.Ok(videoId);
    }
}