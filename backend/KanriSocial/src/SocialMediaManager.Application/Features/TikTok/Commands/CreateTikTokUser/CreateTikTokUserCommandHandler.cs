using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokUser;

public class CreateTikTokUserCommandHandler(
    ITikTokUserRepository tikTokUserRepository, 
    ITokenEncryptor tokenEncryptor, 
    ITikTokClient tikTokClient) : IRequestHandler<CreateTikTokUserCommand, Result<Guid>>
{
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    private readonly ITokenEncryptor _tokenEncryptor = tokenEncryptor;
    private readonly ITikTokClient _tikTokClient = tikTokClient;
    
    public async Task<Result<Guid>> Handle(CreateTikTokUserCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = await _tikTokClient.GetToken(request.Code, TikTokTokenType.AccessToken);
        if (tokenResult.IsFailed)
        {
            return Result.Fail(tokenResult.Errors);
        }

        var encryptedToken = _tokenEncryptor.Encrypt(tokenResult.Value.RefreshToken);

        var tiktokUser = new TikTokUser
        {
            UserId = request.UserId.ToString(),
            Token = encryptedToken
        };
        
        var tikTokUserId = await _tikTokUserRepository.Create(tiktokUser);
        return Result.Ok(tikTokUserId);
    }
}