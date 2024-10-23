using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Application.Services.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramReel;

public class CreateInstagramReelCommandHandler(
    IInstagramMediaService instagramMediaService, 
    IInstagramUserRepository instagramUserRepository,
    IContentStorageService contentStorageService) : IRequestHandler<CreateInstagramReelCommand, Result<Guid>>
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IContentStorageService _contentStorageService = contentStorageService;
    
    public async Task<Result<Guid>> Handle(CreateInstagramReelCommand request, CancellationToken cancellationToken)
    {
        var user = await _instagramUserRepository.GetByUserId(request.UserId);
        if (user is null)
        {
            return Result.Fail("User not found.");
        }
        
        var stream = new MemoryStream(Convert.FromBase64String(request.VideoBytes));
        var videoUrl = await _contentStorageService.UploadInstagramReelAndGetUrl(stream, $"{request.UserId}/{request.ScheduledAt.ToString().Replace(" ", "_")}.mp4");

        var instagramReel = new InstagramReelDto
        {
            VideoUrl = videoUrl,
            Caption = request.Caption,
            ScheduledAt = request.ScheduledAt
        };
        
        var result = await _instagramMediaService.ScheduleReel(instagramReel, user);
        
        return Result.Ok(result.Value);
    }
}