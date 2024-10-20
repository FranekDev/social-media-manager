using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Queries.GetInstagramUserPosts;

public record GetInstagramUserPostsQuery(Guid UserId, int PageNumber, int PageSize) : IRequest<Result<IEnumerable<InstagramMediaDetail>>>;