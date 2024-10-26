using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

namespace SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;

public class CreateFacebookUserCommandHandler(IFacebookUserRepository facebookUserRepository) : IRequestHandler<CreateFacebookUserCommand, Result<Guid>>
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    
    public async Task<Result<Guid>> Handle(CreateFacebookUserCommand request, CancellationToken cancellationToken)
    {
        var facebookUser = new FacebookUser
        {
            AccountId = request.FacebookUserId,
            UserId = request.UserId.ToString(),
            Token = request.Token
        };

        var fbUserId = await _facebookUserRepository.Create(facebookUser);
        
        return Result.Ok(fbUserId);
    }
}