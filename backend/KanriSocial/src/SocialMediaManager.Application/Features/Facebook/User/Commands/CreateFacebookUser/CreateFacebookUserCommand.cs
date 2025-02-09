using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;

public record CreateFacebookUserCommand(string Token,Guid UserId) : IRequest<Result<Guid>>;