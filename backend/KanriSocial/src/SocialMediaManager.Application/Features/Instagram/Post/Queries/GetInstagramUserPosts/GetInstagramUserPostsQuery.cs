using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Post.Queries.GetInstagramUserPosts;

public record GetInstagramUserPostsQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<Result<IEnumerable<InstagramMediaDetail>>>;