using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.User.Queries.GetInstagramUser;

public record GetInstagramUserByUserIdQuery(Guid UserId) : IRequest<Result<InstagramUserDetail>>;