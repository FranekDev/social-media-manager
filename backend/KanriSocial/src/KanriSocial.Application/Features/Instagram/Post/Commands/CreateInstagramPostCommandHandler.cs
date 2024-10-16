using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Commands;

public class CreateInstagramPostCommandHandler(
    IInstagramPostService instagramPostService,
    IInstagramUserRepository instagramUserRepository) : IRequestHandler<CreateInstagramPostCommand, Result<Guid>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    
    public async Task<Result<Guid>> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
    {
        // var instagramUser = await _instagramUserRepository.GetByUserId(request.UserId);

        var post = new InstagramPostDto(Guid.NewGuid(), request.ImageUrl, request.Caption, request.ScheduledAt, request.UserId, false);
        
        return await _instagramPostService.SchedulePost(post);
    }
}

