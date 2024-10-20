using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Commands.CreateInstagramPost;

public class CreateInstagramPostCommandHandler(
    IInstagramPostService instagramPostService,
    IInstagramUserRepository instagramUserRepository) : IRequestHandler<CreateInstagramPostCommand, Result<Guid>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    
    public async Task<Result<Guid>> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
    {
        var post = new InstagramPostDto(Guid.NewGuid(), request.ImageUrl, request.Caption, request.ScheduledAt, request.UserId, false);

        var instagramUser = await _instagramUserRepository.GetByUserId(post.UserId);
        // upload photo to azure
        // uppload photo to db with image link
        // schedule post
        return await _instagramPostService.SchedulePost(post, instagramUser);
    }
}

