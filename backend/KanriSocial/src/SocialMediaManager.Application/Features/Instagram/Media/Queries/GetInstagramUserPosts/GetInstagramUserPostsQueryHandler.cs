using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramUserPosts;

public class GetInstagramUserPostsQueryHandler(IInstagramMediaService instagramMediaService) : IRequestHandler<GetInstagramUserPostsQuery, Result<IEnumerable<InstagramMediaDetail>>>
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;
    
    public async Task<Result<IEnumerable<InstagramMediaDetail>>> Handle(GetInstagramUserPostsQuery request, CancellationToken cancellationToken)
    {
        var result = await _instagramMediaService.GetPostMedia(request.UserId, request.PageNumber, request.PageSize);
        
        return Result.Ok(result);
    }
}