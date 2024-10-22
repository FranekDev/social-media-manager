using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramCommentReplies;

public record GetInstagramCommentRepliesQuery(string CommentId, Guid UserId) : IRequest<Result<IEnumerable<InstagramComment>>>;