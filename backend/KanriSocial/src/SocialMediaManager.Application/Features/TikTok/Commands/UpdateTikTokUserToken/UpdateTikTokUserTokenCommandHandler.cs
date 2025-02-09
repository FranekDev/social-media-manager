using FluentResults;
using MediatR;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Commands.UpdateTikTokUserToken;

public class UpdateTikTokUserTokenCommandHandler(
    ITokenEncryptor tokenEncryptor,
    ITikTokUserRepository tikTokUserRepository,
    ITikTokClient tikTokClient) : IRequestHandler<UpdateTikTokUserTokenCommand, Result>
{
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    
    public async Task<Result> Handle(UpdateTikTokUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (user == null)
        {
            return Result.Fail("User not found.");
        }
        
        var tokenResult = await _tikTokClient.GetToken(request.Code, TikTokTokenType.AccessToken);
        if (tokenResult.IsFailed)
        {
            return Result.Fail(tokenResult.Errors);
        }
        
        var encryptedToken = _tokenEncryptor.Encrypt(tokenResult.Value.RefreshToken);
        user.Token = encryptedToken;
        
        await _tikTokUserRepository.Update(user);
        
        return Result.Ok();
    }
}