using FluentResults;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Application.Services.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Post.Commands.CreateInstagramPost;

public class CreateInstagramPostCommandHandler(
    IInstagramPostService instagramPostService,
    IInstagramUserRepository instagramUserRepository,
    IContentStorageService contentStorageService) : IRequestHandler<CreateInstagramPostCommand, Result<Guid>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IContentStorageService _contentStorageService = contentStorageService;
    
    public async Task<Result<Guid>> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream(Convert.FromBase64String(request.ImageBytes));
        var imageUrl = await _contentStorageService.UploadInstagramPostAndGetUrl(stream, $"{request.UserId}/{request.ScheduledAt.ToString()}.jpg");
        var post = new InstagramPostDto(Guid.NewGuid(), imageUrl, request.Caption, request.ScheduledAt, request.UserId, false);
        var instagramUser = await _instagramUserRepository.GetByUserId(post.UserId);
        
        return await _instagramPostService.SchedulePost(post, instagramUser);
    }
}

