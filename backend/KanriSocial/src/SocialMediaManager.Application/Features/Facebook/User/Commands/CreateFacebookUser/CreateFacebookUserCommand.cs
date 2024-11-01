using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;

public record CreateFacebookUserCommand(string FacebookUserId, string Token,Guid UserId) : IRequest<Result<Guid>>;