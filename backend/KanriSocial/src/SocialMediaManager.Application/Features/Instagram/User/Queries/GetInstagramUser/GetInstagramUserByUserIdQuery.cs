using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.User.Queries.GetInstagramUser;

public record GetInstagramUserByUserIdQuery(Guid UserId) : IRequest<Result<InstagramUserDetail>>;