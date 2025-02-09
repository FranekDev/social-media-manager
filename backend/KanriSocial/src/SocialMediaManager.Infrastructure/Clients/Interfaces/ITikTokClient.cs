using FluentResults;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Infrastructure.Clients.Interfaces;

public interface ITikTokClient
{
    Task<Result<Response<TikTokUserInfo>?>> GetUserInfo(string accessToken);
    Task<Result<TikTokToken?>> GetToken(string token, TikTokTokenType tokenType);
    Task<Result<Response<TikTokPostVideoFromUrl>?>> PostVideo(TikTokPostInfo postInfo, TikTokSurceInfo surceInfo, string accessToken);
    Task<Result<Response<TikTokContentPostResponse>?>> PostContent(TikTokContent content, string accessToken);
    Task<Result<Response<TikTokVideosInfo>?>> GetVideosStats(string accessToken, IEnumerable<string> videoIds);
    Task<Result<Response<TikTokVideos>?>> GetVideos(string accessToken, int maxCount = 20);
}