using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledVideos;

public class GetTikTokScheduledVideosQueryHandler(
    ITikTokVideoRepository tikTokVideoRepository,
    ITikTokUserRepository tikTokUserRepository) : IRequestHandler<GetTikTokScheduledVideosQuery, Result<IEnumerable<TikTokVideoDto>>>
{
    private readonly ITikTokVideoRepository _tikTokVideoRepository = tikTokVideoRepository;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    
    public async Task<Result<IEnumerable<TikTokVideoDto>>> Handle(GetTikTokScheduledVideosQuery request, CancellationToken cancellationToken)
    {
        var tikTokUser = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (tikTokUser == null)
        {
            return Result.Fail("User not found");
        }
        
        var videos = await _tikTokVideoRepository.GetUnpublishedByUserId(request.UserId);
        var videoDtos = videos.Select(MapToDto);
        
        return Result.Ok(videoDtos);
    }
    
    private TikTokVideoDto MapToDto(TikTokVideo video)
    {
        return new TikTokVideoDto
        {
            Id = video.Id,
            Title = video.Title,
            VideoUrl = video.VideoUrl,
            ScheduledAt = video.ScheduledAt
        };
    }
}