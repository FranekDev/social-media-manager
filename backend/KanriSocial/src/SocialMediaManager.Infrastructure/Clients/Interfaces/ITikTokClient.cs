using FluentResults;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Infrastructure.Clients.Interfaces;

public interface ITikTokClient
{
    Task<Result<Response<TikTokUserInfo>?>> GetUserInfo(string accessToken);
    Task<Result<TikTokToken?>> GetToken(string token, TikTokTokenType tokenType);
}