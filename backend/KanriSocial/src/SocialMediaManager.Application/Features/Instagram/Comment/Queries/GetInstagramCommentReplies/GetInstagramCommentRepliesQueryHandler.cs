using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramCommentReplies;

public class GetInstagramCommentRepliesQueryHandler(
    IInstagramMediaService instagramMediaService) : IRequestHandler<GetInstagramCommentRepliesQuery, Result<IEnumerable<InstagramComment>>>
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;

    public async Task<Result<IEnumerable<InstagramComment>>> Handle(GetInstagramCommentRepliesQuery request, CancellationToken cancellationToken)
    {
        var comments = await _instagramMediaService.GetCommentReplies(request.CommentId, request.UserId);
        return Result.Ok(comments);
    }
}
