using FluentResults;
using Hangfire;
using MediatR;
using SocialMediaManager.Application.Extensions;
using SocialMediaManager.Application.Services.Interfaces;
using SocialMediaManager.Application.Services.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Paths;
using SocialMediaManager.Shared.Static.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public class CreateTikTokPhotoCommandHandler(
    ITikTokPhotoRepository photoRepository,
    ITikTokUserRepository tikTokUserRepository,
    IContentStorageService contentStorageService) : IRequestHandler<CreateTikTokPhotoCommand, Result<Guid>>
{
    private readonly ITikTokPhotoRepository _photoRepository = photoRepository;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    private readonly IContentStorageService _contentStorageService = contentStorageService;
    
    public async Task<Result<Guid>> Handle(CreateTikTokPhotoCommand request, CancellationToken cancellationToken)
    {
        var tiktokUser = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (tiktokUser == null)
        {
            return Result.Fail("User not found.");
        }
        
        var photoUrls = new List<string>();
        foreach (var imageBytes in request.ImagesBytes)
        {
            var fileName = $"{tiktokUser.UserId}_{Guid.NewGuid()}.jpg";
            using var stream = new MemoryStream(Convert.FromBase64String(imageBytes));
            var photoUrl = await _contentStorageService.Upload(stream, $"{ContentStoragePath.TikTok.PhotoPath}{fileName}");
            photoUrls.Add(photoUrl);
        }

        var postInfo = new TikTokContentPostInfo(request.Title, request.Description, false, TikTokPrivaceLevel.SelfOnly, false);
        var sourceInfo = new TikTokContentSourceInfo(TikTokPostSource.PullFromUrl, 0, photoUrls);
        
        if (!request.ScheduledAt.IsUtcDateIfNotConvertToUtc(out var scheduledTimeUtc))
        {
            return Result.Fail("Failed to parse scheduled time");
        }
        
        var photoId = await _photoRepository.Create(new Domain.Models.TikTok.TikTokPhoto
        {
            Title = request.Title,
            Description = request.Description,
            ScheduledAt = request.ScheduledAt,
            TikTokUserId = tiktokUser.Id,
            PhotoImagesUrls = photoUrls
        });
        
        var delay = scheduledTimeUtc - DateTime.UtcNow;
        var content = new TikTokContent(postInfo, sourceInfo, TikTokPostMode.DirectPost, TikTokMediaType.Photo);
            
        BackgroundJob.Schedule<ITikTokPhotoService>(x => x.SchedulePhoto(content, tiktokUser, photoId), delay);
        
        return Result.Ok(photoId);
    }
}