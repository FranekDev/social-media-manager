using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreateFacebookPagePost;

public record CreateFacebookPagePostCommand(string PageId, string Message, DateTime ScheduledAt, Guid UserId) : IRequest<Result<Guid?>>;