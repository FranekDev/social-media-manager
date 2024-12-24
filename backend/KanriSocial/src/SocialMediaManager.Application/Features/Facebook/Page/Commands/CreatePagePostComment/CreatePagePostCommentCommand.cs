using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreatePagePostComment;

public record CreatePagePostCommentCommand(string PagePostId, string PostCommentId, string Message, Guid UserId) : IRequest<Result<FacebookNewComment>>;