using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledPhotos;

public class GetTikTokScheduledPhotosQueryHandler(
    ITikTokPhotoRepository tikTokPhotoRepository,
    ITikTokUserRepository tikTokUserRepository) : IRequestHandler<GetTikTokScheduledPhotosQuery, Result<IEnumerable<TikTokPhotoDto>>>
{
    private readonly ITikTokPhotoRepository _tikTokPhotoRepository = tikTokPhotoRepository;
    private readonly ITikTokUserRepository _tikTokUserRepository = tikTokUserRepository;
    
    public async Task<Result<IEnumerable<TikTokPhotoDto>>> Handle(GetTikTokScheduledPhotosQuery request, CancellationToken cancellationToken)
    {
        var tikTokUser = await _tikTokUserRepository.GetByUserId(request.UserId.ToString());
        if (tikTokUser == null)
        {
            return Result.Fail("User not found");
        }
        
        var photos = await _tikTokPhotoRepository.GetUnpublishedByUserId(request.UserId);
        var photoDtos = photos.Select(MapToDto);
        
        return Result.Ok(photoDtos);
    }
    
    private TikTokPhotoDto MapToDto(TikTokPhoto photo)
    {
        return new TikTokPhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            Description = photo.Description,
            PhotoUrls = photo.PhotoImagesUrls,
            ScheduledAt = photo.ScheduledAt
        };
    }
}