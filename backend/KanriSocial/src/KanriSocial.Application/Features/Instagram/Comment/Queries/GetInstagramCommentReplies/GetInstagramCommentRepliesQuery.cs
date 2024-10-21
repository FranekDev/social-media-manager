using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Comment.Queries.GetInstagramCommentReplies;

public record GetInstagramCommentRepliesQuery(string CommentId, Guid UserId) : IRequest<Result<IEnumerable<InstagramComment>>>;