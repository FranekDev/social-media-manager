using FluentResults;
using Hangfire;
using MediatR;
using SocialMediaManager.Application.Extensions;
using SocialMediaManager.Application.Services.Interfaces;
using SocialMediaManager.Application.Services.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Paths;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;

public class CreateTikTokVideoCommandHandler(
    ITikTokUserRepository tikTokUserRepository,
    ITikTokVideoRepository tikTokVideoRepository,
    IContentStorageService contentStorageService) : IRequestHandler<CreateTikTokVideoCommand, Result<Guid>>
{
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    private readonly ITikTokVideoRepository _tikTokVideoRepository = tikTokVideoRepository;
    private readonly IContentStorageService _contentStorageService = contentStorageService;
    
    public async Task<Result<Guid>> Handle(CreateTikTokVideoCommand request, CancellationToken cancellationToken)
    {
        var tiktokUser = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (tiktokUser == null)
        {
            return Result.Fail("User not found.");
        }
       
        var fileName = $"{tiktokUser.UserId}_{Guid.NewGuid()}.mp4";
        using var stream = new MemoryStream(Convert.FromBase64String(request.VideoBytes));
        var videoUrl = await _contentStorageService.Upload(stream, $"{TikTokContentStoragePath.VideoPath}{fileName}");

        var postInfo = new TikTokPostInfo(request.Title, "SELF_ONLY", false, true, false, 1000);
        var sourceInfo = new TikTokSurceInfo("PULL_FROM_URL", videoUrl);
        
        if (!request.ScheduledAt.IsUtcDateIfNotConvertToUtc(out var scheduledTimeUtc))
        {
            return Result.Fail("Failed to parse scheduled time");
        }
        
        var videoId = await _tikTokVideoRepository.Create(new Domain.Models.TikTok.TikTokVideo
        {
            Title = request.Title,
            ScheduledAt = request.ScheduledAt,
            TikTokUserId = tiktokUser.Id,
            VideoUrl = videoUrl
        });
        
        var delay = scheduledTimeUtc - DateTime.UtcNow;
        BackgroundJob.Schedule<ITikTokVideoService>(x => 
            x.ScheduleVideo(postInfo, sourceInfo, tiktokUser, videoId), delay);
        
        return Result.Ok(videoId);
    }
}