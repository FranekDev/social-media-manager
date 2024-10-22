using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.User.Commands.CreateInstagramUser;

public record CreateInstagramUserCommand(Guid UserId, string InstagramUserId, string Token) : IRequest<Guid?>;