using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Commands.CreateCommentReply;

public record CreateCommentReplyCommand(string CommentId, string Message, Guid UserId) : IRequest<Result<InstagramCommentReply>>;