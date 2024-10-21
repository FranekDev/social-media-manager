using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Comment.Commands.CreateCommentReply;

public record CreateCommentReplyCommand(string CommentId, string Message, Guid UserId) : IRequest<Result<InstagramCommentReply>>;