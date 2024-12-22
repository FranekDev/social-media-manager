using MediatR;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedReels;

public class GetUnpublishedReelsQueryHandler(IInstagramReelRepository instagramReelRepository) : IRequestHandler<GetUnpublishedReelsQuery, IEnumerable<InstagramReelDto>>
{
    private readonly IInstagramReelRepository _instagramReelRepository = instagramReelRepository;
    
    public async Task<IEnumerable<InstagramReelDto>> Handle(GetUnpublishedReelsQuery request, CancellationToken cancellationToken)
    {
        var reels = await _instagramReelRepository.GetUnpublishedReels(request.UserId);
        return reels.Select(x => new InstagramReelDto
        {
            Id = x.Id,
            Caption = x.Caption,
            VideoUrl = x.VideoUrl,
            ScheduledAt = x.ScheduledAt
        });
    }
}