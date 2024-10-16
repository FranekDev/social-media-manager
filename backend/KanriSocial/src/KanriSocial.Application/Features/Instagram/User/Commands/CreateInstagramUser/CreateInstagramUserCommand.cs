using MediatR;

namespace KanriSocial.Application.Features.Instagram.User.Commands.CreateInstagramUser;

public record CreateInstagramUserCommand(Guid UserId, string InstagramUserId, string Token) : IRequest<Guid?>;