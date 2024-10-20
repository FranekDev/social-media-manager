using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Queries.GetUnpublishedPosts;

public record GetUnpublishedPostsQuery(Guid UserId) : IRequest<Result<IEnumerable<InstagramPostDto>>>;