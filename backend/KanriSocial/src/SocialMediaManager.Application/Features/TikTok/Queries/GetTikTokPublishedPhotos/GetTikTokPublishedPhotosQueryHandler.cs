using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedPhotos;

public class GetTikTokPublishedPhotosQueryHandler(ITikTokPhotoRepository tikTokPhotoRepository) : IRequestHandler<GetTikTokPublishedPhotosQuery, Result<IEnumerable<TikTokPhotoDto>>>
{
    private readonly ITikTokPhotoRepository _tikTokPhotoRepository = tikTokPhotoRepository;
    
    public async Task<Result<IEnumerable<TikTokPhotoDto>>> Handle(GetTikTokPublishedPhotosQuery request, CancellationToken cancellationToken)
    {
        var photos = await _tikTokPhotoRepository.GetPublishedByUserId(request.UserId);
        return Result.Ok(photos.Select(MapToDto));
    }
    
    private TikTokPhotoDto MapToDto(TikTokPhoto photo)
     => new()
     {
        Id = photo.Id,
        TikTokId = photo.PublishId?.Split(".").Last() ?? "",
        Title = photo.Title,
        Description = photo.Description,
        PhotoUrls = photo.PhotoImagesUrls,
        ScheduledAt = photo.ScheduledAt
     };
}