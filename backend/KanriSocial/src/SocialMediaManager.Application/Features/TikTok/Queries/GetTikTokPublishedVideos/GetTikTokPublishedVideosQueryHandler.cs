using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;
using TikTokVideo = SocialMediaManager.Domain.Models.TikTok.TikTokVideo;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedVideos;

public class GetTikTokPublishedVideosQueryHandler(ITikTokVideoRepository tikTokVideoRepository) : IRequestHandler<GetTikTokPublishedVideosQuery, Result<IEnumerable<TikTokVideoDto>>>
{
    private readonly ITikTokVideoRepository _tikTokVideoRepository = tikTokVideoRepository;
    
    public async Task<Result<IEnumerable<TikTokVideoDto>>> Handle(GetTikTokPublishedVideosQuery request, CancellationToken cancellationToken)
    {
        var videos = await _tikTokVideoRepository.GetPublishedByUserId(request.UserId);
        return Result.Ok(videos.Select(MapToDto));
    }
    
    private TikTokVideoDto MapToDto(TikTokVideo video)
     => new()
     {
        Id = video.Id,
        TikTokId = video.PublishId?.Split(".").Last() ?? "",
        Title = video.Title,
        VideoUrl = video.VideoUrl,
        ScheduledAt = video.ScheduledAt
     };
}