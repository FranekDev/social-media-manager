using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramUserPosts;

public record GetInstagramUserPostsQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<Result<IEnumerable<InstagramMediaDetail>>>;