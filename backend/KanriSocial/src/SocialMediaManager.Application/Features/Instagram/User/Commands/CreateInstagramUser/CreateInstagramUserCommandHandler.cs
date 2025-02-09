using SocialMediaManager.Domain.Models.Instagram.User;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Clients.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.User.Commands.CreateInstagramUser;

public class CreateInstagramUserCommandHandler(
    IInstagramUserRepository instagramUserRepository,
    IInstagramService instagramService,
    UserManager<Domain.Models.User> userManager,
    IInstagramClient instagramClient) : IRequestHandler<CreateInstagramUserCommand, Guid?>
{
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IInstagramService _instagramService = instagramService;
    private readonly UserManager<Domain.Models.User> _userManager = userManager;
    private readonly IInstagramClient _instagramClient = instagramClient;
    
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
        
        var instagramUserData = await _instagramClient.GetUserData(longLivedToken);
        if (instagramUserData.IsFailed)
        {
            return null;
        }

        var instagramUserAccountId = instagramUserData.Value.Data.First().InstagramBusinessAccount.Id;
        
        var igUser = new InstagramUser
        {
            UserId = request.UserId.ToString(),
            AccountId = instagramUserAccountId,
            Token = longLivedToken,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        var instagramUserId = await _instagramUserRepository.Create(igUser);
        return instagramUserId;
    }
}