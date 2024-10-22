using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramCommentReplies;

public class GetInstagramCommentRepliesQueryHandler(
    IInstagramPostService instagramPostService) : IRequestHandler<GetInstagramCommentRepliesQuery, Result<IEnumerable<InstagramComment>>>
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;

    public async Task<Result<IEnumerable<InstagramComment>>> Handle(GetInstagramCommentRepliesQuery request, CancellationToken cancellationToken)
    {
        var comments = await _instagramPostService.GetCommentReplies(request.CommentId, request.UserId);
        return Result.Ok(comments);
    }
}
