using SocialMediaManager.Domain.Models.Instagram.User;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.User.Commands.CreateInstagramUser;

public class CreateInstagramUserCommandHandler(
    IInstagramUserRepository instagramUserRepository,
    IInstagramService instagramService,
    UserManager<Domain.Models.User> userManager) : IRequestHandler<CreateInstagramUserCommand, Guid?>
{
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IInstagramService _instagramService = instagramService;
    private readonly UserManager<Domain.Models.User> _userManager = userManager;
    
    public async Task<Guid?> Handle(CreateInstagramUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return null;
        }
        
        var longLivedToken = await _instagramService.GetLongLivedToken(request.Token);
        if (longLivedToken is null)
        {
            return null;
        }
        
        var igUser = new InstagramUser
        {
            UserId = request.UserId.ToString(),
            AccountId = request.InstagramUserId,
            Token = longLivedToken,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        var instagramUserId = await _instagramUserRepository.Create(igUser);
        return instagramUserId;
    }
}