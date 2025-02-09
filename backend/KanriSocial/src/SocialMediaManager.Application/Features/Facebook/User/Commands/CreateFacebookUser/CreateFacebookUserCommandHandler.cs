using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

namespace SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;

public class CreateFacebookUserCommandHandler(
    IFacebookUserRepository facebookUserRepository,
    IFacebookClient facebookClient) : IRequestHandler<CreateFacebookUserCommand, Result<Guid>>
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookClient _facebookClient = facebookClient;
    
    public async Task<Result<Guid>> Handle(CreateFacebookUserCommand request, CancellationToken cancellationToken)
    {
        var facebookAccountIdResult = await _facebookClient.GetAccountId(request.Token);
        if (facebookAccountIdResult.IsFailed)
        {
            return Result.Fail(facebookAccountIdResult.Errors);
        }
        
        var facebookUser = new FacebookUser
        {
            AccountId = facebookAccountIdResult.Value.Id,
            UserId = request.UserId.ToString(),
            Token = request.Token
        };

        var fbUserId = await _facebookUserRepository.Create(facebookUser);
        
        return Result.Ok(fbUserId);
    }
}