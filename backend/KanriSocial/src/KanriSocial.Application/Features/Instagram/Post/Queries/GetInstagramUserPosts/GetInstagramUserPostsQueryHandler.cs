using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Queries.GetInstagramUserPosts;

public class GetInstagramUserPostsQueryHandler(IInstagramPostService instagramPostService) : IRequestHandler<GetInstagramUserPostsQuery, Result<IEnumerable<InstagramMediaDetail>>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    
    public async Task<Result<IEnumerable<InstagramMediaDetail>>> Handle(GetInstagramUserPostsQuery request, CancellationToken cancellationToken)
    {
        var result = await _instagramPostService.GetPostMedia(request.UserId, request.PageNumber, request.PageSize);
        
        return Result.Ok(result);
    }
}