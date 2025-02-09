using FluentResults;
using MediatR;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokVideosStats;

public class GetTikTokVideosStatsQueryHandler(
    ITokenEncryptor tokenEncryptor,
    ITikTokUserRepository tikTokUserRepository,
    ITikTokClient tikTokClient) : IRequestHandler<GetTikTokVideosStatsQuery, Result<IEnumerable<TikTokVideoInfo>>>
{
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    
    public async Task<Result<IEnumerable<TikTokVideoInfo>>> Handle(GetTikTokVideosStatsQuery request, CancellationToken cancellationToken)
    {
        var user = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (user == null)
        {
            return Result.Fail("Nie znaleziono użytkownika.");
        }
        
        var decryptedToken = _tokenEncryptor.Decrypt(user.Token);
        var refreshTokenResult = await _tikTokClient.GetToken(decryptedToken, TikTokTokenType.RefreshToken);
        
        var userVideos = await _tikTokClient.GetVideos(refreshTokenResult.Value.AccessToken);
        if (userVideos.IsFailed)
        {
            return Result.Fail(userVideos.Errors);
        }
        
        var videoIds = userVideos.Value.Data.Videos.Select(v => v.Id);
        
        var videosStatsResult = await _tikTokClient.GetVideosStats(refreshTokenResult.Value.AccessToken, videoIds);
        if (videosStatsResult.IsFailed)
        {
            return Result.Fail(videosStatsResult.Errors);
        }
        
        return Result.Ok(videosStatsResult.Value?.Data.Videos ?? []);
    }
}