using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Application.Services.Interfaces;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Commands.CreateInstagramPost;

public class CreateInstagramPostCommandHandler(
    IInstagramPostService instagramPostService,
    IInstagramUserRepository instagramUserRepository,
    IBunnyService bunnyService) : IRequestHandler<CreateInstagramPostCommand, Result<Guid>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IBunnyService _bunnyService = bunnyService;
    
    public async Task<Result<Guid>> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream(Convert.FromBase64String(request.ImageBytes));
        var imageUrl = await _bunnyService.UploadInstagramPostAndGetUrl(stream, $"{request.UserId}/{request.ScheduledAt.ToShortDateString()}");
        var post = new InstagramPostDto(Guid.NewGuid(), imageUrl, request.Caption, request.ScheduledAt, request.UserId, false);
        var instagramUser = await _instagramUserRepository.GetByUserId(post.UserId);
        
        return await _instagramPostService.SchedulePost(post, instagramUser);
    }
}

