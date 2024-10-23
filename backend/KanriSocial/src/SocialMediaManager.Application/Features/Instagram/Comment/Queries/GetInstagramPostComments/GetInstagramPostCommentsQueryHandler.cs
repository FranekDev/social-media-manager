using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramPostComments;

public class GetInstagramPostCommentsQueryHandler(
    IInstagramMediaService instagramMediaService) : IRequestHandler<GetInstagramPostCommentsQuery, Result<IEnumerable<InstagramComment>>>
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;
    
    public async Task<Result<IEnumerable<InstagramComment>>> Handle(GetInstagramPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _instagramMediaService.GetPostComments(request.MediaId, request.UserId);
        return Result.Ok(comments);
    }
}