using FluentResults;
using MediatR;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokUserInfo;

public class GetTikTokUserInfoQueryHandler(
    ITikTokClient tikTokClient, 
    ITokenEncryptor tokenEncryptor,
    ITikTokUserRepository tikTokUserRepository) : IRequestHandler<GetTikTokUserInfoQuery, Result<TikTokUserInfo>>
{
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    
    public async Task<Result<TikTokUserInfo>> Handle(GetTikTokUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (user == null)
        {
            return Result.Fail<TikTokUserInfo>("User not found.");
        }
        
        var encryptedToken = _tokenEncryptor.Decrypt(user.Token);
        var refreshTokenResult = await _tikTokClient.GetToken(encryptedToken, TikTokTokenType.RefreshToken);

        var userInfoResult = await _tikTokClient.GetUserInfo(refreshTokenResult.Value.AccessToken);
        if (userInfoResult.IsFailed)
        {
            return Result.Fail<TikTokUserInfo>(userInfoResult.Errors);
        }

        return userInfoResult.Value.Data;
    }
}