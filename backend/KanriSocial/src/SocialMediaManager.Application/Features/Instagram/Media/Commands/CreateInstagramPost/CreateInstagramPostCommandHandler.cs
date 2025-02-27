﻿using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Application.Services.Interfaces;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;

namespace SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramPost;

public class CreateInstagramPostCommandHandler(
    IInstagramMediaService instagramMediaService,
    IInstagramUserRepository instagramUserRepository,
    IContentStorageService contentStorageService) : IRequestHandler<CreateInstagramPostCommand, Result<Guid>>
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IContentStorageService _contentStorageService = contentStorageService;
    
    public async Task<Result<Guid>> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream(Convert.FromBase64String(request.ImageBytes));
        var imageUrl = await _contentStorageService.UploadInstagramPostAndGetUrl(
            stream, $"{request.UserId}/{request.ScheduledAt.ToString()}.jpg");
        var post = new InstagramPostDto(
            Guid.NewGuid(), 
            imageUrl, 
            request.Caption, 
            request.ScheduledAt, 
            request.UserId, 
            false);
        var instagramUser = await _instagramUserRepository.GetByUserId(post.UserId);
        
        return await _instagramMediaService.SchedulePost(post, instagramUser);
    }
}

