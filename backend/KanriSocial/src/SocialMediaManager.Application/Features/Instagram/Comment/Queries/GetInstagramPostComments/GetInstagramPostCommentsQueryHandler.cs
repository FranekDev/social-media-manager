using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramPostComments;

public class GetInstagramPostCommentsQueryHandler(
    IInstagramPostService instagramPostService) : IRequestHandler<GetInstagramPostCommentsQuery, Result<IEnumerable<InstagramComment>>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    
    public async Task<Result<IEnumerable<InstagramComment>>> Handle(GetInstagramPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _instagramPostService.GetPostComments(request.MediaId, request.UserId);
        return Result.Ok(comments);
    }
}